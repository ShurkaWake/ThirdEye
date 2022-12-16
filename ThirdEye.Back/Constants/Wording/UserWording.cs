namespace ThirdEye.Back.Constants.Wording
{
    public static class UserWording
    {
        public const string InvalidLoginDataMessage = "Invalid login and/or password";
        public const string PasswordMissmatchMessage = "Passwords are not the same";
        public const string UnexpectedErrorMessage = "Something went wrong. Please try again";
        public const string NullUserMessage = "No such user";
        public const string EditErrorMessage = "Unable to edit this user";
        public const string InvalidUserOrTokenMessage = "Invalid user or confirmation token";
        public const string EmailConfirmationNeededMessage = "For this action your email must be confirmed";

        public const string EmailConfirmationMessage = "Please confirm your account by <a href='{0}'>clicking here</a>.";
        public const string EmailConfirmationSubject = "Confirm your email";

        public const string EmailRequiredMessage = @"""Email"" is required field";
        public const string InvalidEmailMessage = "Invalid email";
        public const string PasswordRequiredMessage = @"""Password"" is required field";
        public const string RepeatPasswordRequiredMessage = @"""Repeat password"" is required field";
        public const string FirstNameRequiredMessage = @"""First name"" is required field";
        public const string LastNameRequiredMessage = @"""Last name"" is required field";
    }
}
