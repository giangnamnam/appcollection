using System;
using System.Windows.Forms;
using Damany.Windows.Form;

namespace RemoteImaging
{
    public class MessageBoxService : IMessageBoxService
    {
        public DialogResult ShowError(string errorMessage)
        {
            return ShowMessageBox(errorMessage, MessageBoxIcon.Error);
        }

        private DialogResult ShowMessageBox(string errorMessage, MessageBoxIcon icon, MessageBoxButtons button = MessageBoxButtons.OK)
        {
            Form activeForm = FormHelper.GetActiveForm();

            Func<DialogResult> doit = () => MessageBox.Show(
                activeForm,
                errorMessage,
                activeForm.Text,
                button,
                icon);

            if (activeForm.InvokeRequired)
            {
                return (DialogResult) activeForm.Invoke(doit);
            }
            else
            {
                return  doit.Invoke();
            }
        }

        public DialogResult ShowInfo(string errorMessage)
        {
            return  ShowMessageBox(errorMessage, MessageBoxIcon.Information);
        }

        public DialogResult ShowQuestion(string question, MessageBoxButtons buttons = MessageBoxButtons.YesNo)
        {
            return ShowMessageBox(question, MessageBoxIcon.Question, buttons);
        }

        public void ShowForm(Func<Form> creator)
        {
            var activeForm = FormHelper.GetActiveForm();
            activeForm.Invoke(new Action(() =>
                                                  {
                                                      var form = creator();
                                                      form.ShowDialog(activeForm);
                                                  }));
        }

        public void ShowForm(Form form)
        {
            var activeForm = FormHelper.GetActiveForm();
            if (activeForm != null)
            {
                activeForm.BeginInvoke(new Action(() => form.ShowDialog(activeForm)));
            }
            else
            {
                form.ShowDialog();
            }


        }


      
    }
}