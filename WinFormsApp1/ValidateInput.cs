using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

public static class ValidateInput
{
    public static bool checkTheName(TextBox txtName)
    {
        string pattern = @"[а-яА-Яa-zA-Z]+\s{1,}[а-яА-Яa-zA-Z]+$";
        bool isValid = Regex.IsMatch(txtName.Text, pattern);
        return isValid;

    }

    public static bool onlyNumbersInCard(TextBox txtCardNumber)
    {
        bool onlyDigitsInCardNumber = txtCardNumber.Text.All(char.IsDigit);
        return onlyDigitsInCardNumber && (txtCardNumber.Text.Length == 5);
    }
    public static string formRightCardNumber(int? cardNumber)
    {
        int numberOfNulls = (5 - cardNumber.ToString().Length);
        string nulls = "";
        string result = cardNumber.ToString();
        for (int i = 0; i < numberOfNulls; i++)
        {
            nulls += "0";
        }
        return nulls + result;
    }


}
   