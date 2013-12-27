using System;
using System.Text;

namespace NetDuinoUtils
{
    //The following code was adapted from http://netmfcommonext.codeplex.com/
    //I have made very little changes, as I only needed the Format method,
    //and I did not want to include a dependency.

    //Please check out the full project from the link, its really amazing.
    //Because of namespacing, it should be safe to use the NetMFCommonExt library with
    //this project.

    public static class Extensions
    {
        


        public static string Format(this string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            if (args == null)
                throw new ArgumentNullException("args");

            // Validate the structure of the format string.
            ValidateFormatString(format);

            StringBuilder bld = new StringBuilder();

            int endOfLastMatch = 0;
            int starting = 0;

            while (starting >= 0)
            {
                starting = format.IndexOf('{', starting);

                if (starting >= 0)
                {
                    if (starting != format.Length - 1)
                    {
                        if (format[starting + 1] == '{')
                        {
                            // escaped starting bracket.
                            starting = starting + 2;
                            continue;
                        }
                        else
                        {
                            bool found = false;
                            int endsearch = format.IndexOf('}', starting);

                            while (endsearch > starting)
                            {
                                if (endsearch != (format.Length - 1) && format[endsearch + 1] == '}')
                                {
                                    // escaped ending bracket
                                    endsearch = endsearch + 2;
                                }
                                else
                                {
                                    if (starting != endOfLastMatch)
                                    {
                                        string t = format.Substring(endOfLastMatch, starting - endOfLastMatch);
                                        t = t.Replace("{{", "{"); // get rid of the escaped brace
                                        t = t.Replace("}}", "}"); // get rid of the escaped brace
                                        bld.Append(t);
                                    }

                                    // we have a winner
                                    string fmt = format.Substring(starting, endsearch - starting + 1);

                                    if (fmt.Length >= 3)
                                    {
                                        fmt = fmt.Substring(1, fmt.Length - 2);

                                        string[] indexFormat = fmt.Split(new char[] { ':' });

                                        string formatString = string.Empty;

                                        if (indexFormat.Length == 2)
                                        {
                                            formatString = indexFormat[1];
                                        }

                                        int index = 0;

                                        // no format, just number
                                        if (TryParseInt(indexFormat[0], out index))
                                        {
                                            bld.Append(FormatParameter(args[index], formatString));
                                        }
                                        else
                                        {
                                            throw new FormatException(FormatException.ERROR_MESSAGE);
                                        }
                                    }

                                    endOfLastMatch = endsearch + 1;

                                    found = true;
                                    starting = endsearch + 1;
                                    break;
                                }


                                endsearch = format.IndexOf('}', endsearch);
                            }
                            // need to find the ending point

                            if (!found)
                            {
                                throw new FormatException(FormatException.ERROR_MESSAGE);
                            }
                        }
                    }
                    else
                    {
                        // invalid
                        throw new FormatException(FormatException.ERROR_MESSAGE);
                    }

                }

            }

            // copy any additional remaining part of the format string.
            if (endOfLastMatch != format.Length)
            {
                bld.Append(format.Substring(endOfLastMatch, format.Length - endOfLastMatch));
            }

            return bld.ToString();
        }

        public static bool TryParseInt(string s, out int i)
        {
            i = 0;
            try
            {
                i = int.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void ValidateFormatString(string format)
        {
            char expected = '{';

            int i = 0;

            while ((i = format.IndexOfAny(new char[] { '{', '}' }, i)) >= 0)
            {
                if (i < (format.Length - 1) && format[i] == format[i + 1])
                {
                    // escaped brace. continue looking.
                    i = i + 2;
                    continue;
                }
                else if (format[i] != expected)
                {
                    // badly formed string.
                    throw new FormatException(FormatException.ERROR_MESSAGE);
                }
                else
                {
                    // move it along.
                    i++;

                    // expected it.
                    if (expected == '{')
                        expected = '}';
                    else
                        expected = '{';
                }
            }

            if (expected == '}')
            {
                // orpaned opening brace. Bad format.
                throw new FormatException(FormatException.ERROR_MESSAGE);
            }

        }

        private static string FormatParameter(object p, string formatString)
        {
            if (formatString == string.Empty)
                return p.ToString();

            if (p as IFormattable != null)
            {
                return ((IFormattable)p).ToString(formatString, null);
            }
            else if (p is DateTime)
            {
                return ((DateTime)p).ToString(formatString);
            }
            else if (p is Double)
            {
                return ((Double)p).ToString(formatString);
            }
            else if (p is Int16)
            {
                return ((Int16)p).ToString(formatString);
            }
            else if (p is Int32)
            {
                return ((Int32)p).ToString(formatString);
            }
            else if (p is Int64)
            {
                return ((Int64)p).ToString(formatString);
            }
            else if (p is SByte)
            {
                return ((SByte)p).ToString(formatString);
            }
            else if (p is Single)
            {
                return ((Single)p).ToString(formatString);
            }
            else if (p is UInt16)
            {
                return ((UInt16)p).ToString(formatString);
            }
            else if (p is UInt32)
            {
                return ((UInt32)p).ToString(formatString);
            }
            else if (p is UInt64)
            {
                return ((UInt64)p).ToString(formatString);
            }
            else
            {
                return p.ToString();
            }

        }

        public static string Replace(this string content, string find, string replace)
        {
            int startFrom = 0;
            int findItemLength = find.Length;

            int firstFound = content.IndexOf(find, startFrom);
            var returning = new StringBuilder();

            string workingString = content;

            while ((firstFound = workingString.IndexOf(find, startFrom)) >= 0)
            {
                returning.Append(workingString.Substring(0, firstFound));
                returning.Append(replace);

                // the remaining part of the string.
                workingString = workingString.Substring(firstFound + findItemLength, workingString.Length - (firstFound + findItemLength));
            }

            returning.Append(workingString);

            return returning.ToString();

        }
    }



    public class FormatException : Exception
    {
        internal static string ERROR_MESSAGE = "Format string is not valid.";

        /// <summary>
        /// Initializes a new instance of the FormatException class.
        /// </summary>
        public FormatException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FormatException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public FormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FormatException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="ex">The exception that is the cause of the current exception. If the innerException parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
        public FormatException(string message, Exception ex)
            : base(message, ex)
        {
        }

    }
}
