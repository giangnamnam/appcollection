using System;
using System.Windows.Forms;

namespace RemoteImaging
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowError(string errorMessage)
        {
            ShowMessageBox(errorMessage, MessageBoxIcon.Error);
        }

        private void ShowMessageBox(string errorMessage, MessageBoxIcon icon)
        {
            Form activeForm = GetActiveForm();

            Action doit = () => MessageBox.Show(
                activeForm,
                errorMessage,
                activeForm.Text,
                MessageBoxButtons.OK,
                icon);

            if (activeForm.InvokeRequired)
            {
                activeForm.Invoke(doit);
            }
            else
            {
                doit.Invoke();
            }
        }

        public void ShowInfo(string errorMessage)
        {
            ShowMessageBox(errorMessage, MessageBoxIcon.Information);
        }

        public void ShowForm(Func<Form> creator)
        {
            var activeForm = GetActiveForm();
            activeForm.Invoke(new Action(() =>
                                                  {
                                                      var form = creator();
                                                      form.ShowDialog(activeForm);
                                                  }));
        }

        public void ShowForm(Form form)
        {
            var activeForm = GetActiveForm();
            if (activeForm != null)
            {
                activeForm.BeginInvoke(new Action(() => form.ShowDialog(activeForm)));
            }
            else
            {
                form.ShowDialog();
            }


        }


        private Form GetActiveForm()
        {
            return Application.OpenForms[0];

        }
    }
}