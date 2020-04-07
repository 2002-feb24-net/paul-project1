using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Logic
{
    public static class BadWordChecker
    {
        private static List<string> BadNameList = new List<string> {"This is going to be a long throwaway string that censors the words for anyone looking a this code in visual studio code. Yes this is a very very simple censor system but you get the idea.                                      ","fuck","bitch","penis","vagina","nigger","negro","bastard","cunt","faggot","fag","retard","pussy","testicle","cock","whore","blowjob","idiot","slut","chink","dildo","masturbate","boob","boobies","booby","boobys","@$$","cuck","autism","autistic","douche","douchebag","asshole"};

        public static bool CheckWord(string inputWord)
        {
            foreach (var val in BadNameList)
            {

                if(inputWord.ToLower().Contains(val))
                {
                    return false;
                }
            }
            return true;
        }
    }
}