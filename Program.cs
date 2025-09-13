var solution = new Solution();
int[] nums = { 5, 10, 15, 20, 25 };
var result = solution.MinSubsetDifference(nums);

Console.WriteLine("Subset 1: " + string.Join(", ", result.Item1));
Console.WriteLine("Subset 2: " + string.Join(", ", result.Item2));

var diff = Math.Abs(result.Item1.Sum() - result.Item2.Sum());
Console.WriteLine($"Difference: {diff}"); // 5

public class Solution
{
    public Tuple<List<int>, List<int>>? MinSubsetDifference(int[] nums)
    {
        if (nums == null || nums.Length < 2) return null;

        var totalSum = 0;
        foreach (int num in nums) totalSum += num;

        var target = totalSum / 2;
        var dp = new bool[target + 1];
        dp[0] = true; // Empty subset sums to 0

        // Build DP table
        for (var i = 0; i < nums.Length; i++)
        {
            for (int j = target; j >= nums[i]; j--)
            {
                dp[j] = dp[j] || dp[j - nums[i]];
            }
        }

        // Find the largest achievable sum <= target
        var closestSum = 0;
        for (int i = target; i >= 0; i--)
        {
            if (dp[i])
            {
                closestSum = i;
                break;
            }
        }

        // Reconstruct subsets
        var subset1 = new List<int>();
        var subset2 = new List<int>();
        var remainingSum = totalSum;

        for (var i = nums.Length - 1; i >= 0; i--)
        {
            if (closestSum >= nums[i] && dp[closestSum - nums[i]])
            {
                subset1.Add(nums[i]);
                closestSum -= nums[i];
            }
            else
            {
                subset2.Add(nums[i]);
            }
        }

        return new Tuple<List<int>, List<int>>(subset1, subset2);
    }
}