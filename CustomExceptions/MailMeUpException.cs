namespace CustomExceptions
{
    public class MailMeUpException : Exception
    {
        public MailMeUpException(string message) : base(message)
        {

        }

        public MailMeUpException()
        {

        }

        public MailMeUpException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}