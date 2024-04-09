using System.Diagnostics;

namespace Battleships.Game
{
    public class Utilities
    {
        public static List<List<int>> GetConsecutiveGroups(List<int> list)
        {
            var consecutiveGroups = new List<List<int>>();
            try
            {
                var orderedList = list.Order().ToList();
                var consecutiveGroup = new List<int>();

                consecutiveGroup.Add(orderedList[0]);

                for (int i = 1; i < orderedList.Count; i++)
                {
                    if (orderedList[i] - orderedList[i - 1] == 1)
                    {
                        consecutiveGroup.Add(orderedList[i]);
                    }
                    else
                    {
                        if (consecutiveGroup.Count > 1)
                        {
                            consecutiveGroups.Add(consecutiveGroup);
                        }
                        consecutiveGroup = new List<int>();
                        consecutiveGroup.Add(orderedList[i]);
                    }
                    if (i == orderedList.Count - 1 && consecutiveGroup.Count > 1)
                    {
                        consecutiveGroups.Add(consecutiveGroup);
                    }

                }
                return consecutiveGroups;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return consecutiveGroups;
            }
        }

        public static int GetLetterIndex(char letter)
        {
            char upper = char.ToUpper(letter);
            return upper - 'A';
        }

        // Not used. Code below would be used as the basis to develop range finding functionality.
        // Returns a euclidean distance letting the user know how close missing shots are to the target.

        static double CalculateDistance(int[] p1, int[] p2)
        {
            double dx = p2[0] - p1[0];
            double dy = p2[1] - p1[1];

            return Math.Sqrt(dx * dx + dy * dy);
        }

    }
}
