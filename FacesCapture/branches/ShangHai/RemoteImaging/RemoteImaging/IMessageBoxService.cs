namespace RemoteImaging
{
    public interface IMessageBoxService
    {
        void ShowError(string errorMessage);
        void ShowInfo(string errorMessage);

        void ShowForm(System.Func<System.Windows.Forms.Form> creator);
    }
}