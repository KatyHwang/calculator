using System;
using System.Collections.Generic; 
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Operator
        /// </summary>
        private List<string> OperatorList = new List<string>();

        /// <summary>
        /// number
        /// </summary>
        private List<double> numberList = new List<double>();

        private void btntotal_Click(object sender, EventArgs e)
        {
            ////分成兩個陣列 數字和運算值
            ////char [] a= { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };//int不用 ""or''
            //char[] b = { '+', '-', '*', '//' };
            ////b[0] = '+'; b[1] = '-'; b[2] = '*'; b[3] = '/'; //取出符號存放在陣列 ,陣列從0開始算
            //string[] name = lablshow.Text.Split(b); //對暫存數字串的那格 做切割(Split) 把符號提出來分別放在陣列裡
            ////string[] name2 = textBox1.Text.Split(a); //對textbox字串做切割(Split) 把數字提出來分別放在陣列裡
            //List<string> numberList = new List<string>(name); //Array轉list //放數字
            //if (log == 0)
            //{

            CheckNumber();

            operation(numberList, OperatorList);  

            OperatorList.Clear(); 

            if (numberList.Count != 0)
            {
                lablshow.Text = numberList[0].ToString(); //把運算完的數字(numberList)放進labl顯示 //[0]正負號整數
                textBox1.Text = numberList[0].ToString();  //暫存上一個加總,ex.總值=7,下一個輸入2 = 7+2
            }
            numberList.Clear();//清除數字
            lablshow.Text = ""; //對按下等於後殘留在上方的數字做清除
                               
        }


        private void btn0_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "0")
            {
                textBox1.Text = "";
                lablshow.Text = "";
            }
            else if(textBox1.Text=="+"|| textBox1.Text == "-" || textBox1.Text == "*" || textBox1.Text == "/" )
            {
                lablshow.Text += textBox1.Text;
                Add_substring(textBox1.Text);//呼叫方法 ("+")<=s的計算符號  
                textBox1.Text = "";
            }
            textBox1.Text += ((Button)sender).Text;//按鍵顯示在textbox上
        }


        #region 四則運算
        //做運算//建立新的群組 為運算用(operation意思:運算)  //()裡面放參數
        double Total;  
        /// <summary>
        /// 四則運算
        /// <para>說明:第一個參數為數字LIST , 第二個參數為符號LIST</para>
        /// </summary>
        /// <param name="numberList"></param>
        /// <param name="OperatorList"></param>
        public void operation(List<double> numberList, List<string> OperatorList)
        {
            List<string> op = new List<string>(new string[] { "*", "/", "-", "+" }); 
            foreach (string pp in op)  
            {
                while (OperatorList.Contains(pp))  
                {
                    int num1 = OperatorList.IndexOf(pp);
                    int num2 = OperatorList.IndexOf(pp) + 1;  

                    Total = 0;
                    double k = numberList[num1]; 
                    double j = numberList[num2];
                    if (pp == "*")
                    {
                        Total = k*j;
                    }
                    else if (pp == "/")
                    {
                        Total = k/j;
                    }
                    else if (pp == "-")
                    {
                        Total = k-j;
                    }
                    else

                    {
                        Total = k+j;
                    }
                    //清除計算過數值
                    numberList.RemoveAt(num2);
                    OperatorList.RemoveAt(num1);
                    numberList[num1] = Total;
                    if (OperatorList.Contains(pp))
                    {
                        continue;//如有相同運算符號,則回while
                    }
                }
            }
        }

        #endregion
        //_____________________________運算符號事件________________________________

        #region 運算符號事件
        /// <summary>
        /// 新增運算符號
        /// </summary>
        /// <param name="s"></param>
        public void Add_substring(string s) 
        {
            OperatorList.Add(s); 
        }

        /// <summary>
        /// 新增數字到list
        /// </summary>
        /// <param name="s"></param>
        public void Add_number(double s) //放變數接收值
        {
            numberList.Add(s);//增加陣列內容(s)到list裡面  //放數字
        }

        /// <summary>
        /// 確認是否為數字，若是則加入數字list，並將值給予textbox1
        /// </summary>
        private void CheckNumber()
        {
            double n;
            if (double.TryParse(textBox1.Text, out n)) 
            {
                Add_number(Convert.ToDouble(textBox1.Text));
                lablshow.Text += textBox1.Text;
            }
        }

        #endregion

       
        private void button12_Click(object sender, EventArgs e) 
        {
            //HACK:新增確認數字的方法
            CheckNumber();

            //HACK:將運算的按鍵統一為BUTTON12
            textBox1.Text = ((Button)sender).Text; 
        }
   
        private void CE_Click(object sender, EventArgs e) 
        {
            textBox1.Text = "";
            lablshow.Text = "";
            OperatorList.Clear();
            numberList.Clear();
        }

        private void dot_Click(object sender, EventArgs e) 
        {
            if (textBox1.Text.IndexOf(".") < 0)
            {
                textBox1.Text += ".";
            }
        }

        private void btnnegative_Click(object sender, EventArgs e)
        {
            double n;
            if (double.TryParse(textBox1.Text, out n))//判斷字串是否為數字
            {
                if (textBox1.Text.IndexOf("-") >= 0)
                {
                    textBox1.Text = textBox1.Text.Replace("-", "");
                }
                else
                {
                    textBox1.Text = "-" + textBox1.Text;
                }

            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            if (s.Length > 1)
            {
                textBox1.Text = (double.Parse(s.Substring(0, s.Length - 1))).ToString();
            }
            else
            {
                textBox1.Text = "0"; 
            }
        }
    }
}
