using System;
using System.Collections.Generic; 
using System.Windows.Forms;

namespace 小算盤_陣列
{
    public partial class Form1 : Form
    {
      // int log = 0; //設定一個參數為0
        public Form1()
        {
            InitializeComponent();
        }
        //用兩個list寫,一個存數字一個存符號
        /// <summary>
        /// 符號List
        /// </summary>
        private List<string> namestring = new List<string>(); //Array轉list //放在全域變數裡面 //放符號

        /// <summary>
        /// 數字List
        /// </summary>
        private List<double> namestring2 = new List<double>();//數字

        private void btntotal_Click(object sender, EventArgs e)
        {
            ////分成兩個陣列 數字和運算值
            ////char [] a= { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };//int不用 ""or''
            //char[] b = { '+', '-', '*', '//' };
            ////b[0] = '+'; b[1] = '-'; b[2] = '*'; b[3] = '/'; //取出符號存放在陣列 ,陣列從0開始算
            //string[] name = lablshow.Text.Split(b); //對暫存數字串的那格 做切割(Split) 把符號提出來分別放在陣列裡
            ////string[] name2 = textBox1.Text.Split(a); //對textbox字串做切割(Split) 把數字提出來分別放在陣列裡
            //List<string> namestring2 = new List<string>(name); //Array轉list //放數字
            //if (log == 0)
            //{

            CheckNumber();


            operation(namestring2, namestring); //進入運算 參數位置要一樣
            namestring.Clear();//清除符號

            if (namestring2.Count != 0)
            {
                lablshow.Text = namestring2[0].ToString(); //把運算完的數字(namestring2)放進labl顯示 //[0]正負號整數
                textBox1.Text = namestring2[0].ToString();  //暫存上一個加總,ex.總值=7,下一個輸入2 = 7+2
            }
            namestring2.Clear();//清除數字
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
        double c; //總和
        /// <summary>
        /// 四則運算
        /// <para>說明:第一個參數為數字LIST , 第二個參數為符號LIST</para>
        /// </summary>
        /// <param name="namestring2"></param>
        /// <param name="namestring"></param>
        public void operation(List<double> namestring2, List<string> namestring)
        {
            List<string> op = new List<string>(new string[] { "*", "/", "-", "+" });//建立新的list 放運算符號
            foreach (string pp in op) //先乘除後加減
            {
                while (namestring.Contains(pp)) //找list裡面有沒有xx值,有的話進入迴圈
                {
                    int o = namestring.IndexOf(pp);
                    int oo = namestring.IndexOf(pp) + 1; //namestng2 數字

                    c = 0;
                    double k = namestring2[o];//namestring符號
                    double j = namestring2[oo];
                    if (pp == "*")
                    {
                        c = k*j;
                    }
                    else if (pp == "/")
                    {
                        c = k/j;
                    }
                    else if (pp == "-")
                    {
                        c = k-j;
                    }
                    else

                    {
                        c = k+j;
                    }
                    //清除計算過數值
                    namestring2.RemoveAt(oo);
                    namestring.RemoveAt(o);
                    namestring2[o] = c;
                    if (namestring.Contains(pp))
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
        public void Add_substring(string s) //放變數接收值 
        {
            namestring.Add(s);//增加陣列內容(s)到list裡面  //放符號
        }

        /// <summary>
        /// 新增數字到list
        /// </summary>
        /// <param name="s"></param>
        public void Add_number(double s) //放變數接收值
        {
            namestring2.Add(s);//增加陣列內容(s)到list裡面  //放數字
        }

        /// <summary>
        /// 確認是否為數字，若是則加入數字list，並將值給予textbox1
        /// </summary>
        private void CheckNumber()
        {
            double n;
            if (double.TryParse(textBox1.Text, out n))//判斷字串是否為數字
            {
                Add_number(Convert.ToDouble(textBox1.Text));
                lablshow.Text += textBox1.Text;
            }
        }

        #endregion

       
        private void button12_Click(object sender, EventArgs e)//運算按鍵
        {
            //HACK:新增確認數字的方法
            CheckNumber();

            //HACK:將運算的按鍵統一為BUTTON12
            textBox1.Text = ((Button)sender).Text; 
        }
   
        private void CE_Click(object sender, EventArgs e)//清除所有值
        {
            textBox1.Text = "";
            lablshow.Text = "";
            namestring.Clear();
            namestring2.Clear();
        }

        private void dot_Click(object sender, EventArgs e) //小數點
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
