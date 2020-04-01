using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace BVAV
{
        //按钮
        private void search_Click(object sender, EventArgs e)
        {
            //查询文本框内是否有内容
            if(string.IsNullOrEmpty(UUID.Text))
            {
                MessageBox.Show("请输入BV号", "标题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if((UUID.Text) == "请输入BV号")
            {
                MessageBox.Show("请输入BV号", "标题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String url = "https://api.bilibili.com/x/web-interface/view?bvid=[REPLACE]";
            String Url = url.Replace("[REPLACE]", UUID.Text);

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Url);
            Request.Timeout = 20 * 1000;//请求超时。
            Request.AllowAutoRedirect = true; //网页自动跳转。
            Request.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";//伪装成谷歌爬虫。
            Request.Method = "GET"; //获取数据的方法。GET
                                    //Request.Method = "POST";//POST
                                    //Request.ContentType = "application/json";上传的格式JSON
            Request.KeepAlive = true; //保持
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            using (StreamReader sReader = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                String Htmlstring = sReader.ReadToEnd();

                int i = Htmlstring.IndexOf("\"aid\":");
                int j = Htmlstring.IndexOf(",\"videos\"");
                Htmlstring = (Htmlstring.Substring(i + 6)).Substring(0, j - i - 6);
                ht.Text = "AV号是:av" + Htmlstring;
            }

        }
    }
}
