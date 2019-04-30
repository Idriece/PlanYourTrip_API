using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripException
{
    public class NoRecordFoundException : Exception
    {
        private readonly Dictionary<string, string> error;

        public NoRecordFoundException(Dictionary<string, string> errorItem)
        {
            this.error = errorItem;
        }

        public NoRecordFoundException(string errorCode, string errorMessage) :this(new Dictionary<string, string>() { { errorCode, errorMessage} })
        {

        }

        public NoRecordFoundException(IDictionary<string, string> error): base (message: string.Format(CultureInfo.InvariantCulture, "{0}{1}", "One or more business rule violations occurred: " + Environment.NewLine, string.Join(Environment.NewLine, error.Values.ToArray())))
        {

        }

    }
}
