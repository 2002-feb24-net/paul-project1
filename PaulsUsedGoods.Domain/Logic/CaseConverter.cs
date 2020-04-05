namespace PaulsUsedGoods.Domain.Logic
{
    public static class CaseConverter
    {
        public static string Convert(string inputString)
        {
            inputString = inputString.ToLower();
            if (inputString.Length == 1)
            {
                inputString = inputString.ToUpper();
            }
            else
            {
                string[] inputArray = inputString.Split(" ");
                for (int i = 0; i< inputArray.Length; i++)
                {
                    inputArray[i] = char.ToUpper(inputArray[i][0]) + inputArray[i].Substring(1);
                }
                inputString = string.Join(' ', inputArray);
            }
            return inputString;
        }
    }
}