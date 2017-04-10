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

namespace CheckInAppV3
{
    /*class for UI controls appearence*/
    public sealed class LoginView
    {
        public bool LoginIsHidden(EditText emailTB)
        {
            if (emailTB.Visibility.Equals(ViewStates.Gone))
                return true;
            else
                return false;
        }

        public bool SignUpIsHidden(EditText signUpEmail)
        {
            if (signUpEmail.Visibility.Equals(ViewStates.Gone))
                return true;
            else
                return false;
        }

        public void HideSignUp(EditText signUpEmail, EditText signUpLogin, EditText signUpPassword, LinearLayout bottomLayout)
        {
            signUpEmail.Visibility = ViewStates.Gone;
            signUpLogin.Visibility = ViewStates.Gone;
            signUpPassword.Visibility = ViewStates.Gone;
            bottomLayout.Visibility = ViewStates.Visible;
        }

        public void HideLogin(EditText emailTB, EditText passTB)
        {
            emailTB.Visibility = ViewStates.Gone;
            passTB.Visibility = ViewStates.Gone;
        }

        public void OpenLogin(EditText emailTB, EditText passTB)
        {
            emailTB.Visibility = ViewStates.Visible;
            passTB.Visibility = ViewStates.Visible;
        }

        public void OpenSignUp(EditText signUpEmail, EditText signUpLogin, EditText signUpPassword, LinearLayout bottomLayout)
        {
            signUpEmail.Visibility = ViewStates.Visible;
            signUpLogin.Visibility = ViewStates.Visible;
            signUpPassword.Visibility = ViewStates.Visible;
            bottomLayout.Visibility = ViewStates.Gone;
        }
    }
}