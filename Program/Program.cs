﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Написать программу «симулятор лифта».
 * При запуске прграммы в качестве параметров задается:
 *      кол-во этажей в подъезде - N
 *      скорость лифта при движении в метрах в секунду (ускорением пренебрегаем, считаем, что когда лифт едет - он сразу едет с определенной скоростью);
 *      время между открытием и закрытием дверей.
 *
 * После запуска программа должна постоянно ожидать ввода от пользователя и выводить действия лифта в реальном времени.
 * События, которые нужно выводить:
 * лифт проезжает некоторый этаж;
 * лифт открыл двери;
 * лифт закрыл двери.
 *
 * Возможный ввод пользователя:
 *       вызов лифта на этаж из подъезда;
 *       нажать на кнопку этажа внутри лифта.
 *
 * Считаем, что пользователь не может помешать лифту закрыть двери.
 * Все данные, которых не хватает в задаче можно выбрать на свое усмотрение.
 * В результате должен получиться компилируемый код (в случае с java предлагается писать код в одном файле).
 * а в случае если это си шарп то он ддолжен быть разбит на каждый логический кусок программы в каждом файле
 * Доп. Сделать визуализацию 
 */
namespace Program
{
    internal static class Program
    {
        
        /**
         * Главная точка входа для приложения.
         */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
