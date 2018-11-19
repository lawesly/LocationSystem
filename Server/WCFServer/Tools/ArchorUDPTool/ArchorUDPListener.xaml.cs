﻿using Coldairarrow.Util.Sockets;
using DbModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TModel.Tools;

namespace ArchorUDPTool
{
    /// <summary>
    /// ArchorUDPListener.xaml 的交互逻辑
    /// </summary>
    public partial class ArchorUDPListener : Window
    {
        public ArchorUDPListener()
        {
            InitializeComponent();
        }

        LightUDP udp;
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            var ip = CbIps.Text;
            var port = TbPort.Text;
            
            if (BtnStart.Content.ToString() == "开始")
            {
                udp = new LightUDP(ip, port.ToInt());
                udp.DGramRecieved += Udp_DGramRecieved;
                BtnStart.Content = "停止";
            }
            else
            {
                udp.Close();
                BtnStart.Content = "开始";
            }
            
        }

        private void Udp_DGramRecieved(object sender, BUDPGram dgram)
        {
            string txt = string.Format("[{0}][{1}]{2}({3})",DateTime.Now.ToString("HH:mm:ss.fff"),dgram.iep,ByteHelper.byteToHexStr(dgram.data),ByteHelper.byteToStr(dgram.data));
            TbConsole.Text = txt + "\n" + TbConsole.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CbIps.ItemsSource = IpHelper.GetLocalList();
            CbIps.SelectedIndex = 0;
        }
    }
}
