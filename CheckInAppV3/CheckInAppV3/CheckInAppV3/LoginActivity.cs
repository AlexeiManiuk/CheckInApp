using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;
using Android.Animation;
using CheckInAppV3.checkInRef;

namespace CheckInAppV3
{
    [Activity(Label = "LoginActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LoginActivity : Android.Support.V7.App.AppCompatActivity
    {
        CheckInAppV3.checkInRef.Check_InService client;
        public LoginActivity()
        {
            client = new Check_InService();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            /*Creating item objects*/
            /*Buttons for login and sign up*/
            var signUpButt = FindViewById<Button>(Resource.Id.SignInButt);
            var loginButt = FindViewById<Button>(Resource.Id.LoginButt);
            /*Text boxes for loging in process*/
            var emailTB = FindViewById<EditText>(Resource.Id.EmailTB);
            var passTB = FindViewById<EditText>(Resource.Id.PassTB);
            /*Text boxes for signing up process*/
            var signUpLogin = FindViewById<EditText>(Resource.Id.SignUpLogin);
            var signUpPassword = FindViewById<EditText>(Resource.Id.SignUpPassword);
            var signUpEmail = FindViewById<EditText>(Resource.Id.SignUpEmail);
            /*Initializing bottom social network butons*/
            var vkButt = FindViewById<ImageButton>(Resource.Id.VKButt);
            var gpButt = FindViewById<ImageButton>(Resource.Id.GPButt);
            var fbButt = FindViewById<ImageButton>(Resource.Id.FBButt);
            var bottomLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);

            /*OnClick event handler for Login button*/
            loginButt.Click += (s, e) =>
            {
                if (!LoginView.SignUpIsHidden(signUpEmail))
                {
                    LoginView.HideSignUp(signUpEmail, signUpLogin, signUpPassword, bottomLayout);
                }
                if (LoginView.LoginIsHidden(emailTB))
                {
                    LoginView.OpenLogin(emailTB, passTB);
                }
                else
                {
                    if (Validator.ValidationChecker(emailTB, passTB))
                    {
                        Validator.ServiceRespond = client.LoginAccess(emailTB.Text, passTB.Text);
                    }
                    if (Validator.ServiceRespond.Equals(true))
                    {
                        var activity = new Intent(this, typeof(MapActivity));
                        activity.PutExtra("Login", emailTB.Text);
                        StartActivity(activity);
                    }
                    else if(Validator.ServiceRespond.Equals(false))
                    {
                        Validator.InvalidData(passTB);
                    }
                }
            };

            /*OnClick handler for sign up button*/
            signUpButt.Click += (e, s) => 
            {
                if (!LoginView.LoginIsHidden(emailTB))
                {
                    LoginView.HideLogin(emailTB, passTB);
                }
                if (LoginView.SignUpIsHidden(signUpEmail))
                {
                    LoginView.OpenSignUp(signUpEmail, signUpLogin, signUpPassword, bottomLayout);
                }
                else
                {
                    if (Validator.ValidationChecker(signUpEmail, signUpPassword))
                    {
                        Validator.ServiceRespond = client.SignUpUser(signUpLogin.Text, signUpPassword.Text, signUpEmail.Text);
                    }
                    if (Validator.ServiceRespond.Equals(true))
                    {
                        var activity = new Intent(this, typeof(MapActivity));
                        activity.PutExtra("Login", emailTB.Text);
                        StartActivity(activity);
                    }
                    else if (Validator.ServiceRespond.Equals(false))
                    {
                        Validator.InvalidData(signUpPassword);
                    }
                }
            };
        }
    }
}