namespace TaskApplicationJIRA.Models.ChangePasswordModel
{
    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }  // User identifier, if needed
        public string CurrentPassword { get; set; }  // Formerly OldPassword
        public string NewPassword { get; set; }  // New password
        public string ConfirmPassword { get; set; }  // Confirm the new password
    }
}
