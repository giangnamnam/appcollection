namespace RemoteImaging
{
    public interface IMessageBoxService
    {
        System.Windows.Forms.DialogResult ShowError(string errorMessage);
        System.Windows.Forms.DialogResult ShowInfo(string errorMessage);
        System.Windows.Forms.DialogResult ShowQuestion(string question, System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.YesNo);

        void ShowForm(System.Func<System.Windows.Forms.Form> creator);
    }
}