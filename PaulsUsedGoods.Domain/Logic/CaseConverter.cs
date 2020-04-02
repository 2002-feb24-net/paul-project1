namespace PaulsUsedGoods.Domain.Logic
{
    public static class CaseConverter
    {
        public static string Convert(string inputString)
        {
            inputString = inputString.ToLower();
            inputString = inputString[0].ToString().ToUpper() + inputString.Substring(1,inputString.Length);
            return inputString;
        }
    }
}