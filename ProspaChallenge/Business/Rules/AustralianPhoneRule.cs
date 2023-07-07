using ProspaChallenge.Business.Interfaces;
using System.Text.RegularExpressions;

namespace ProspaChallenge.Business.Rules
{
    public interface IPhoneRule : IRule<string> { }

    public class AustralianPhoneRule : IPhoneRule
    {
        public bool IsQualifiedFor(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return false;
            return IsMobileNumber(phoneNumber) || IsLandlineNumber(phoneNumber);
        }

        private bool IsMobileNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
            {
                if (!phoneNumber.StartsWith("+614")) return false;
                if (phoneNumber.Length != 12) return false;

                return Regex.IsMatch(phoneNumber.Substring(4), @"^\d+$");
            }

            if (!phoneNumber.StartsWith("04")) return false;
            return Regex.IsMatch(phoneNumber.Substring(2), @"^\d+$");
        }
        private bool IsLandlineNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10) return false;

            var landlinePrefix = new List<string>()
            {
                "02", "03", "07", "08"
            };
            var isOfLandlinePrefix = landlinePrefix.All(pf => phoneNumber.StartsWith(pf));
            if (!isOfLandlinePrefix) return false;

            return Regex.IsMatch(phoneNumber.Substring(2), @"^\d+$");
        }

        public bool IsUnqualifiedFor(string validationTarget)
        {
            throw new NotImplementedException();
        }
    }
}
