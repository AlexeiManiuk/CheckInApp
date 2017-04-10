using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Mail;

namespace CheckInAppV3
{
    public static class Validator
    {
        public static bool? ServiceRespond { get; set; }

        static Validator()
        {
        }

        public static bool ValidationChecker(EditText emailTB, EditText passTB)
        {
            if (PassIsEmpty(passTB))
            {
                return false;
            }
            return InvalidEmail(emailTB);
        }

        public static bool PassIsEmpty(EditText passTB)
        {
            if (passTB.Equals(String.Empty))
            {
                EmptyPassHint(passTB);
                return true;
            }
            return false;
        }

        public static void EmptyPassHint(EditText passTB)
        {
            passTB.Text = String.Empty;
            passTB.Hint = "Password cannot be empty";
            passTB.SetHintTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Red));
        }

        public static bool InvalidEmail(EditText emailTB)
        {
            try
            {
                MailAddress address = new MailAddress(emailTB.Text);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is ArgumentNullException)
                {
                    emailTB.Text = String.Empty;
                    emailTB.Hint = "E-mail is invalid";
                    emailTB.SetHintTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Red));
                }
                return false;
            }
        }

        public static void InvalidData(EditText passTB)
        {
            passTB.Text = String.Empty;
            passTB.Hint = "Invalid email or password";
            passTB.SetHintTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Red));
        }
    }
}