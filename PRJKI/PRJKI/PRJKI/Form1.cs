using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace PRJKI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ��ư Paint �̺�Ʈ ���
            button1.Paint += new PaintEventHandler(this.Button_Paint);
            button2.Paint += new PaintEventHandler(this.Button_Paint);
            button3.Paint += new PaintEventHandler(this.Button_Paint);

            // �� ��ư�� �ٸ� �̹��� ��� �� �ؽ�Ʈ ����
            button1.Tag = new { ImagePath = "C:\\Users\\user\\Desktop\\img\\join.png", Text = "�ű� ȸ�� ����" };
            button2.Tag = new { ImagePath = "C:\\Users\\user\\Desktop\\img\\member.png", Text = "ȸ�� ����" };
            button3.Tag = new { ImagePath = "C:\\Users\\user\\Desktop\\img\\Card.png", Text = "��ȸ�� ����" };

            // ���׶� ��ư �����
            MakeRoundButton(button1);
            MakeRoundButton(button2);
            MakeRoundButton(button3);
        }

        private void Button_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // ��Ƽ�ٸ���� ����
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // �Ͼ�� ��� ä���
                e.Graphics.FillEllipse(Brushes.White, 0, 0, button.Width, button.Height);

                // �׵θ� �׸��� (���� ����)
                e.Graphics.DrawEllipse(new Pen(Color.Gray, 1), 0, 0, button.Width, button.Height);

                // Tag �Ӽ����� �̹��� ��ο� �ؽ�Ʈ ��������
                dynamic tagInfo = button.Tag;
                string imagePath = tagInfo.ImagePath;
                string buttonText = tagInfo.Text;

                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    // �̹��� ���� �ε�
                    Image buttonImage = Image.FromFile(imagePath);

                    // �̹����� ��ư �߾ӿ� ���߾� �׸���
                    int imageX = (button.Width - buttonImage.Width) / 2;
                    int imageY = (button.Height - buttonImage.Height) / 4;  // ��ܿ� ��ġ
                    e.Graphics.DrawImage(buttonImage, new Rectangle(imageX, imageY, buttonImage.Width, buttonImage.Height));
                }

                // �ؽ�Ʈ �׸���
                Font font = new Font("���� ���", 10);  // ��Ʈ ����
                SizeF textSize = e.Graphics.MeasureString(buttonText, font);

                // �ؽ�Ʈ�� �̹��� �Ʒ� �߾ӿ� ��ġ
                int textX = (button.Width - (int)textSize.Width) / 2;
                int textY = (button.Height / 2) + 20;  // �̹��� �ٷ� �Ʒ��� �ؽ�Ʈ ��ġ
                e.Graphics.DrawString(buttonText, font, Brushes.Black, new PointF(textX, textY));
            }
        }

        private void MakeRoundButton(Button button)
        {
            // ��ư�� �������� ����
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, button.Width, button.Height);
            button.Region = new Region(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();  // Form2 �ν��Ͻ� ����
            form2.Show();               // Form2�� ȭ�鿡 ǥ��
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();  // Form3 �ν��Ͻ� ����
            form3.Show();               // Form3�� ȭ�鿡 ǥ��
            this.Hide();
        }
    }
}