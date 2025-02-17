﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new AddPages.Autho());
        }
    }

    public class Films
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AgeId { get; set; }
        public Age Age { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }


    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class Age
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Phone {  get; set; }
    }

}
