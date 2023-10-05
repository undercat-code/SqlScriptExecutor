using System.Text.RegularExpressions;

namespace SqlScriptExecutor.Core
{
    public class LogCollectionReformator
    {
        public List<string> ReformatErrorText(List<string> logCollection)
        {
            var patternErrorFilter = @"\bError in\b";
            var ErrorCollection = new List<string>();

            foreach (var log in logCollection)
            {
                bool errorFilter = Regex.IsMatch(log, patternErrorFilter);

                if (errorFilter)
                {
                    ErrorCollection.Add(log);
                }
            }

            //Delete Stack Trace from Error text
            var patternDeleteStackTrace = @"   \bat ";
            var deletedStackTraceCollection = new List<string>();

            foreach (var error in ErrorCollection)
            {
                var splitStackTrace = Regex.Split(error, patternDeleteStackTrace);
                deletedStackTraceCollection.Add(splitStackTrace[0]);
            }

            //Final split for Path and Error text
            var patternSplitPathAndErrors = @"\b:";
            var splitedPathAndErrorsCollection = new List<string>();

            foreach (var error in deletedStackTraceCollection)
            {
                var splitPathAndErrors = Regex.Split(error, patternSplitPathAndErrors);


                foreach (var errorText in splitPathAndErrors)
                {
                    //Remove "Error in" from Path text
                    var removeErrorIn = errorText.Replace("Error in ", "");
                    splitedPathAndErrorsCollection.Add(removeErrorIn);

                }

            }


            return splitedPathAndErrorsCollection;
        }
    }
}
