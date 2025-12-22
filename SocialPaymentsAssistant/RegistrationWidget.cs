using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocialPaymentsAssistant
{
    public partial class RegistrationWidget : Form
    {
        public RegistrationWidget()
        {
            InitializeComponent();

            // Устанавливаем начальное состояние кнопки "Продолжить"
            UpdateNextButtonState();

            // Подключаем обработчики событий для радио-кнопок
            empRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            applicantRadioButton.CheckedChanged += RadioButton_CheckedChanged;

            // Подключаем обработчики для кнопок
            loginButton.Click += LoginButton_Click;
            nextButton.Click += NextButton_Click;

            // Настраиваем событие закрытия формы
            this.FormClosed += RegistrationWidget_FormClosed;
        }

        private void RegistrationWidget_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNextButtonState();
        }

        private void UpdateNextButtonState()
        {
            // Активируем кнопку "Продолжить", только если выбрана одна из ролей
            bool hasSelection = empRadioButton.Checked || applicantRadioButton.Checked;
            nextButton.Enabled = hasSelection;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Переход к окну входа (точно как в C++)
            LoginWidget loginWidget = new LoginWidget();
            loginWidget.Show();
            this.Close(); // Закрываем окно регистрации
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            // Проверяем, какая роль выбрана
            if (empRadioButton.Checked)
            {
                // Открываем окно регистрации сотрудника
                EmpReg empReg = new EmpReg();

                // Настраиваем событие закрытия формы EmpReg
                empReg.FormClosed += (s, args) =>
                {
                    // При закрытии EmpReg ничего не делаем
                    // Если нужно вернуться к выбору типа, можно создать новый RegistrationWidget
                };

                empReg.Show();
                this.Close(); // Закрываем окно выбора типа регистрации
            }
            else if (applicantRadioButton.Checked)
            {
                // Открываем окно регистрации заявителя
                ApplicantReg applicantReg = new ApplicantReg();

                // Настраиваем событие закрытия формы ApplicantReg
                applicantReg.FormClosed += (s, args) =>
                {
                    // При закрытии ApplicantReg ничего не делаем
                };

                applicantReg.Show();
                this.Close(); // Закрываем окно выбора типа регистрации
            }
            else
            {
                // Ничего не выбрано - показываем предупреждение
                MessageBox.Show("Пожалуйста, выберите тип аккаунта для регистрации.",
                    "Выбор типа аккаунта", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}