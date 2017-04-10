using Android.Views;
using Android.Widget;

namespace CheckInAppV3
{
    /*class for UI controls appearence*/
    public static class LoginView
    {
        public static bool LoginIsHidden(EditText emailTb)
        {
            if (emailTb.Visibility.Equals(ViewStates.Gone))
                return true;
            else
                return false;
        }

        public static bool SignUpIsHidden(EditText signUpEmail)
        {
            return signUpEmail.Visibility.Equals(ViewStates.Gone);
        }

        public static void HideSignUp(EditText signUpEmail, EditText signUpLogin, EditText signUpPassword, LinearLayout bottomLayout)
        {
            signUpEmail.Visibility = ViewStates.Gone;
            signUpLogin.Visibility = ViewStates.Gone;
            signUpPassword.Visibility = ViewStates.Gone;
            bottomLayout.Visibility = ViewStates.Visible;
        }

        public static void HideLogin(EditText emailTb, EditText passTb)
        {
            emailTb.Visibility = ViewStates.Gone;
            passTb.Visibility = ViewStates.Gone;
        }

        public static void OpenLogin(EditText emailTb, EditText passTb)
        {
            emailTb.Visibility = ViewStates.Visible;
            passTb.Visibility = ViewStates.Visible;
        }

        public static void OpenSignUp(EditText signUpEmail, EditText signUpLogin, EditText signUpPassword, LinearLayout bottomLayout)
        {
            signUpEmail.Visibility = ViewStates.Visible;
            signUpLogin.Visibility = ViewStates.Visible;
            signUpPassword.Visibility = ViewStates.Visible;
            bottomLayout.Visibility = ViewStates.Gone;
        }
    }
}