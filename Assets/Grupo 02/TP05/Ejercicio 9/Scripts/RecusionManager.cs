using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecusionManager : MonoBehaviour
{
    [Header("UI References")]

    public TMP_InputField inputField;
    public TMP_Text outputText;

    public void OnFibonacciButton()
    {
        if (int.TryParse(inputField.text, out int n))
        {
            string result = "";
            for (int i = 0;  i < n; i++)
            {
                result += Fibonacci(i) + (i < n - 1 ? ", " : "");
            }
            outputText.text = $"Fibonacci(({n}): {result}";
        }
        else
        {
            outputText.text = "Please enter a valid number.";
        }
    }

    public void OnFactorialButton()
    {
        if (int.TryParse(inputField.text, out int n))
        {
            outputText.text = $"Factorial({n}) = {Factorial(n)}";
        }
        else
        {
            outputText.text = "Please enter a valid number.";
        }
    }

    public void OnSumButton()
    {
        if (int.TryParse(inputField.text, out int n))
        {
            outputText.text = $"Sum(1..{n}) = {Sum(n)}";
        }
        else
        {
            outputText.text = "Please enter a valid number.";
        }
    }

    public void OnPyramidButton()
    {
        if (int.TryParse(inputField.text, out int n))
        {
            string pyramid = BuildPyramid(n);
            outputText.text = pyramid;
        }
        else
        {
            outputText.text = "Please enter a valid number.";
        }
    }

    public void OnPalindromeButton()
    {
        string phrase = inputField.text.ToLower().Replace(" ", "");
        bool isPalindrome = IsPalindrome(phrase, 0, phrase.Length - 1);
        outputText.text = isPalindrome ? "Yes, it is a palindrome." : "No, it is not a palindrome.";
    }

    private int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    private int Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }

    private int Sum(int n)
    {
        if (n <= 0) return 0;
        return (n - 1) + Sum(n - 1);
    }

    private string BuildPyramid(int height, int current = 1)
    {
        if (current > height) return "";
        int totalWidth = height * 2 - 1;
        int xCount = current * 2 - 1;
        string line = new string(' ', (totalWidth - xCount) / 2) + new string('x', xCount);
        return line + "\n" + BuildPyramid(height, current + 1);
    }

    private bool IsPalindrome(string s, int left, int right)
    {
        if (left >= right) return true;
        if (s[left] != s[right]) return false;
        return IsPalindrome(s, left + 1, right - 1);
    }
}
