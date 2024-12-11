using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PRJKI
{
    public partial class Form7 : Form
    {
        private string phoneNumber;  // 현재 로그인된 사용자의 전화번호
        private decimal currentBalance;  // 현재 잔액
        private decimal pointBalance = 0;  // 포인트 잔액 (기본값은 0으로 설정)
        private decimal updatedCashBalance; // 충전 후 업데이트된 현금 잔액
        private decimal availableBalance;  // 결제 가능 잔액

        // Form7 생성자: 전화번호와 현재 잔액을 받음
        public Form7(string phoneNumber, decimal currentBalance)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
            this.currentBalance = currentBalance;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // 안내문 표시
            lblInstruction.Text = $"현재 잔액: {currentBalance:C}";
        }

        // 충전 버튼 클릭 이벤트
        private void btnConfirmCharge_Click(object sender, EventArgs e)
        {
            decimal chargeAmount;

            // 충전할 금액이 올바른 숫자인지 확인
            if (decimal.TryParse(txtChargeAmount.Text, out chargeAmount) && chargeAmount > 0)
            {
                // 충전 후 잔액을 업데이트하고 Form4를 엽니다
                UpdateBalance(chargeAmount);

                // 충전 완료 후 Form4로 이동
                OpenForm4();
            }
            else
            {
                MessageBox.Show("유효한 금액을 입력해주세요.");
            }
        }

        // 잔액을 업데이트하는 메서드
        private void UpdateBalance(decimal chargeAmount)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=plus;Uid=root;Pwd=1234;"))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET cash_balance = cash_balance + @ChargeAmount WHERE phone_number = @PhoneNumber";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ChargeAmount", chargeAmount);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.ExecuteNonQuery();
                    }

                    // 충전 성공 메시지 및 새로운 잔액 표시
                    updatedCashBalance = currentBalance + chargeAmount;  // 충전 후 잔액 업데이트
                    availableBalance = updatedCashBalance + pointBalance;  // 결제 가능 잔액 계산
                    lblInstruction.Text = $"충전 완료! 현재 잔액: {updatedCashBalance:C}";
                    MessageBox.Show($"잔액이 충전되었습니다! 충전 금액: {chargeAmount:C}, 현재 잔액: {updatedCashBalance:C}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("충전 중 오류가 발생했습니다: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Form4로 이동 시 필요한 데이터를 전달
            Form4 form4 = new Form4(phoneNumber, updatedCashBalance, pointBalance, availableBalance);
            form4.Show();
            this.Hide();  // 현재 폼을 숨김
        }

        private void OpenForm4()
        {
            // Form4로 이동 시 필요한 데이터를 여기에 맞게 전달
            Form4 form4 = new Form4(phoneNumber, updatedCashBalance, pointBalance, availableBalance);
            form4.Show();
            this.Close();  // Form7을 닫습니다
        }
    }
}
