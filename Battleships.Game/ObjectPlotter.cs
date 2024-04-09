using System.Diagnostics;

namespace Battleships.Game
{
    // Large scope for refactoring for efficiency, testability and readability.
    public class ObjectPlotter
    {
        public static List<Tuple<int, int>> GetRange2d(int requiredPoints, int size, List<Tuple<int, int>> excludedPoints)
        {
            var returnList = new List<Tuple<int, int>>();

            var indexCounter = size - 1;

            if (requiredPoints > size * size - excludedPoints.Count)
            {
                return returnList;
            }

            var maxX = indexCounter;
            var maxY = indexCounter;

            int startX = 0;
            int startY = 0;

            int possibleX;
            int possibleY;

            var random = new Random();

            var orientationRand = new Random();
            var orientation = orientationRand.Next(2);

            try
            {
                // Code for finding fit would be generalised so one method would work across dimensions in
                // later development. Below is a PoC, the requirment being that it reliably works to support
                // development. Generalisation would require much more time. Calculations would be factored
                // out to Utilities or another injectable service to enable reuse and independent testing.
                // Generalised code would open the possibility for 3D game play, so submarines and aircraft
                // could also be included.
                if (orientation == 0)
                {
                    possibleX = random.Next(maxX);

                    var allRows = Enumerable.Range(0, size).ToList();

                    bool willNotFit = true;
                    do
                    {
                        var excludedByRow =
                            excludedPoints
                            .Where(p => p.Item1 == possibleX)
                            .OrderBy(p => p.Item2)
                            .Select(p => p.Item2).ToList();
                        var wholeRow = Enumerable.Range(0, size).ToList();
                        var possibleRow = wholeRow.Except(excludedByRow);

                        var consecutiveCells = Utilities.GetConsecutiveGroups(possibleRow.ToList());

                        if (consecutiveCells.Any(cc => cc.Count >= requiredPoints))
                        {
                            startX = possibleX;
                            var selectRange = consecutiveCells.First(cc => cc.Count >= requiredPoints);
                            startY = selectRange.First();
                            willNotFit = false;
                        }
                        else
                        {
                            // To be isolated to enable seperate development and testing.
                            // May be adding a bias towards the top and left of the board.
                            allRows.Remove(possibleX);
                            if (allRows.Count == 0)
                            {
                                return returnList;
                            }
                            possibleX = allRows.First();
                        }
                    }
                    while (willNotFit);

                    for (int i = 0; i < requiredPoints; i++)
                    {
                        returnList.Add(new Tuple<int, int>(startX, startY));
                        startY++;
                    }
                }
                else
                {
                    // Look in excluded points - any on that col?
                    possibleY = random.Next(maxY);

                    var allCols = Enumerable.Range(0, size).ToList();

                    bool willNotFit = true;
                    do
                    {
                        var excludedByCol =
                            excludedPoints
                            .Where(p => p.Item2 == possibleY)
                            .OrderBy(p => p.Item1)
                            .Select(p => p.Item1).ToList();
                        var wholeCol = Enumerable.Range(0, size).ToList();
                        var possibleCol = wholeCol.Except(excludedByCol);

                        var consecutiveCells = Utilities.GetConsecutiveGroups(possibleCol.ToList());

                        if (consecutiveCells.Any(cc => cc.Count >= requiredPoints))
                        {
                            startY = possibleY;
                            var selectRange = consecutiveCells.First(cc => cc.Count >= requiredPoints);
                            startX = selectRange.First();
                            willNotFit = false;
                        }
                        else
                        {
                            allCols.Remove(possibleY);
                            if (allCols.Count == 0)
                            {
                                return returnList;
                            }
                            possibleY = allCols.First();
                        }
                    }
                    while (willNotFit);

                    for (int i = 0; i < requiredPoints; i++)
                    {
                        returnList.Add(new Tuple<int, int>(startX, startY));
                        startX++;
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return returnList;
            }
            return returnList;
        }

    }
}
