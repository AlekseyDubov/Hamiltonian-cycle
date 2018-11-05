using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Гамильтонов_цикл
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] c;   // номер шага, на котором посещается вершина
        int[] path; // номера посещаемых вершин
        int v0 = 2, n;//начальная вершина
        int[,] a;//матрица смежности
        void prnt()
        {
            int p;
            for (p = 0; p < n; p++)
                listBox2.Items.Add(path[p]);
            listBox2.Items.Add(path[0]);
        }
        int gamilton(int k)//Функция гамильтонового цикл
        {
            int v, q1 = 0;
            for (v = 0; v < n && !Convert.ToBoolean(q1); v++)
            {
                if (Convert.ToBoolean(a[v, path[k - 1]]) || Convert.ToBoolean(a[path[k - 1], v]))//Если вершины не равны 0, то есть соеденины
                {
                    //Номер элемента=кол-ву заданных вершин
                    if (k == n && v == v0) //Выход из цикла
                        q1 = 1;
                    else 
                        if (c[v] == -1)//не использованный ход
                    {
                        c[v] = k;//номер посещеного шага=номеру элемента
                        path[k] = v;//Сохраняем номер посещенной вершины
                        q1 = gamilton(k + 1);//рекурсивный вызов
                        if (!Convert.ToBoolean(q1)) 
                            c[v] = -1;//шаг не использван
                    }
                }
            } return q1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка на гамильтонов цикл, по м
            n = (int)(numericUpDown1.Value);//количество вершин
            path = new int[n];
            c = new int[n];
            int i, j, p;
            string elem;
            a = new int[n, n];
            dataGridView1.Rows.Clear();
            listBox2.Items.Clear();
            dataGridView1.RowCount = n + 1;//количество столбцов
            dataGridView1.ColumnCount = n + 1;//кол-во строк
            dataGridView1.Rows[0].Cells[0].Value = "Граф:";
            dataGridView1.Columns[0].Width = 110;//ширина ячейки
            //Разметка матрицы
            for (i = 0; i < n; i++)
            {
                dataGridView1.Rows[i + 1].Cells[0].Value = ("Вершина:" + Convert.ToString(i));
                dataGridView1.Columns[i + 1].Width = 110;
            }
            for (j = 0; j < n; j++)
            {
                dataGridView1.Rows[0].Cells[j + 1].Value = ("Вершина:" + Convert.ToString(j));
            }
            int l;
            l = 1;
           // Задание матрицы смежности. 1-вершины соеденины. 0-не соеденины.
            for (i = 0; i < n; i++)
            {
                for (j = l; j < n; j++)
                {
                    elem = Microsoft.VisualBasic.Interaction.InputBox("Введите элемент.1-Вершины соеденинины.0-Вершины не соеденины" + (i) + ":" + (j));
                    while (Convert.ToInt32(elem) != 0 && Convert.ToInt32(elem) != 1)
                    {
                        MessageBox.Show("Не правильно введены данные.Введите их еще раз");
                        elem = Microsoft.VisualBasic.Interaction.InputBox("Введите элемент.1-Вершины соеденинины.0-Вершины не соеденины" + (i) + ":" + (j));
                    }
                    if (elem.Length > 0)//для возможности отмены
                    {
                        while (true)
                        {
                            try
                            {
                                a[i,j] = Convert.ToInt32(elem);//задание элемента матрицы
                                break;
                            }
                            catch
                            {
                                MessageBox.Show("Не правильно введены данные.");
                                elem = Microsoft.VisualBasic.Interaction.InputBox("Введите элемент.1-Вершины соеденинины.0-Вершины не соеденины " + (i) + ":" + (j));
                                if (elem.Length <= 0)
                                {
                                    return;
                                }
                                while (Convert.ToInt32(elem) != 0 && Convert.ToInt32(elem) != 1)//Проверка на 0 и 1
                                {
                                    MessageBox.Show("Не правильно введены данные.Введите их еще раз");
                                    elem = Microsoft.VisualBasic.Interaction.InputBox("Введите элемент.1-Вершины соеденинины.0-Вершины не соеденины" + (i) + ":" + (j));
                                }
                            }
                        }
                    }
                    dataGridView1.Rows[i+1].Cells[j+1].Value = a[i, j];//Добавление элемента в дата гридвью
                    a[j, i] = a[i, j];//матрица смежности-семетрична
                    dataGridView1.Rows[j + 1].Cells[i + 1].Value = a[j, i];
                }
                l++;
            }
            //Заполняем главную диагональ нулями. Вершина не может быть соеденина сама с собой.
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if(i==j)
                        dataGridView1.Rows[i + 1].Cells[j + 1].Value = 0;
                }
            }
            //Сам гамильтонов цикл
            for (j = 0; j < n; j++) 
                c[j] = -1;//номер хода -1, расчет номера хода начинается с 0
            path[0] = v0;//Первая посещенная вершина 2
            c[v0] = v0;//2 шаг на котором посещается вершина.
            //Выводим посещенные вершины
            if (Convert.ToBoolean(gamilton(1)))
            {
                for (p = 0; p < n; p++)
                    listBox2.Items.Add(path[p]);
                listBox2.Items.Add(path[0]);
            }
            else
                MessageBox.Show("Данный граф не гамильтонов.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n = 10;//количество вершин
            path = new int[n];
            c = new int[n];
            int i, j, p;
            a = new int[,]//Предварительное задание графа(матрицы смежности)
            {
                {0,0,0,0,0,1,0,0,0,0},
                {0,0,1,0,0,0,1,0,0,0},
                {0,1,0,1,0,0,0,1,0,0},
                {0,0,1,0,1,0,0,0,1,0},
                {1,0,0,1,0,0,0,0,0,1},
                {0,0,0,0,0,0,1,0,0,1},
                {0,0,0,1,0,0,0,1,0,0},
                {0,0,0,0,1,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,0,0,0,0,0}
            };
            //Очищение
            dataGridView1.Rows.Clear();
            listBox2.Items.Clear();
            //Задание столбцов и строк в графическом элементе.
            dataGridView1.RowCount = n + 1;
            dataGridView1.ColumnCount = n + 1;
            dataGridView1.Rows[0].Cells[0].Value = "Граф:";//0 ячейка-граф
            dataGridView1.Columns[0].Width = 110;//ширина ячейки
            for (i = 0; i < n; i++)
            {
                dataGridView1.Rows[i + 1].Cells[0].Value = ("Вершина:" + Convert.ToString(i));
                dataGridView1.Columns[i + 1].Width = 110;
            }
            //Разметка вершин
            for (j = 0; j < n; j++)
            {
                dataGridView1.Rows[0].Cells[j + 1].Value = ("Вершина:" + Convert.ToString(j));
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i + 1].Cells[j + 1].Value = a[i, j];
                }
            }
            //Ufvbkmnjyjd wbrk
            for (j = 0; j < n; j++)
                c[j] = -1;
            path[0] = v0;
            c[v0] = v0;
            if (Convert.ToBoolean(gamilton(1)))
            {
                for (p = 0; p < n; p++)
                    listBox2.Items.Add(path[p]);
                listBox2.Items.Add(path[0]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
