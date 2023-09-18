using System.Text.RegularExpressions;

namespace EmailService
{
    public class LogCollectionReformator
    {
        public List<string> ErrorCollection { get; set; }
        public LogCollectionReformator(List<string> errorCollection)
        {
            ErrorCollection = errorCollection;
        }

        public List<string> ReformatErrorText()
        {


            //Delete Stack Trace from Error text
            var patternDeleteStackTrace = @"   \bat ";
            var DeletedStackTraceCollection = new List<string>();

            foreach (var error in ErrorCollection)
            {
                var splitStackTrace = Regex.Split(error, patternDeleteStackTrace);
                DeletedStackTraceCollection.Add(splitStackTrace[0]);
            }

            //Final split for Path and Error text
            var patternSplitPathAndErrors = @"\b:";
            var splitedPathAndErrorsCollection = new List<string>();

            foreach (var error in DeletedStackTraceCollection)
            {
                var splitPathAndErrors = Regex.Split(error, patternSplitPathAndErrors);


                foreach (var error2 in splitPathAndErrors)
                {
                    //Remove "Error in" from Path text
                    var removeErrorIn = error2.Replace("Error in ", "");
                    splitedPathAndErrorsCollection.Add(removeErrorIn);

                }

            }


            return splitedPathAndErrorsCollection;
        }
    }
}
