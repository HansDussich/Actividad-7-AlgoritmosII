using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace actividad7
{
    public partial class Form1 : Form
    {

        SqlConnection conexion = new SqlConnection("server=DESKTOP-A38J5RV ; database=db_actividad7 ; integrated security = true");
        public Form1()
        {
            InitializeComponent();
            mostrar();
            
        }

        // Mostrar tabla en el listBox
        private void mostrar()
        {
            conexion.Open();
            string cadena = "select * from tb_frutas";

            SqlCommand comand = new SqlCommand(cadena, conexion);

            SqlDataReader registros = comand.ExecuteReader();

            while (registros.Read())
            {

                textBox1.AppendText(registros["id"].ToString());
                textBox1.AppendText(" - ");
                textBox1.AppendText(registros["nombre"].ToString());
                textBox1.AppendText(" - ");
                textBox1.AppendText(registros["precio"].ToString());

                textBox1.AppendText(Environment.NewLine);

            }
            conexion.Close();
        }

        // Boton buscar 
        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string cod = txt_buscar.Text;
            string cadena = "select * from tb_frutas where nombre='" + cod + "'";
            SqlCommand comand = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comand.ExecuteReader();
            if (registro.Read())
            {
                textBox1.Clear();
                textBox1.AppendText(registro["id"].ToString());
                textBox1.AppendText(" - ");
                textBox1.AppendText(registro["nombre"].ToString());
                textBox1.AppendText(" - ");
                textBox1.AppendText(registro["precio"].ToString());
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("El nombre de la fruta no fue encontrada");

            }
            conexion.Close();

        }


        //Boton eliminar elemento 
        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string cod = txt_buscar.Text;
            string cadena = "delete from tb_frutas where nombre='" + cod + "'";
            SqlCommand comand = new SqlCommand(cadena, conexion);

            int cant;
            cant = comand.ExecuteNonQuery();
            if (cant == 1)
            {
                MessageBox.Show("se ha borrado " + cod + " del inventario");
            }
            else
            {
                MessageBox.Show("no se ha encontrado la fruta " + cod);
            }
            conexion.Close();
            textBox1.Clear();
            mostrar();
            
        }

        //Boton modificar 
        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string cod = txt_buscar.Text;
            string newPrice = textBox2.Text;

            string cadena = "update tb_frutas set precio= " + newPrice + " where nombre ='" + cod + "'";
            SqlCommand comand = new SqlCommand( cadena, conexion);
            int cant;
            cant = comand.ExecuteNonQuery();
            if (cant == 1)
            {
                MessageBox.Show("Se ha modificado el precio a " + cod);
            }
            else
            {
                MessageBox.Show("No se ha encontrado la fruta");
            }

            conexion.Close();
            textBox1.Clear(); 
            mostrar(); 
        }
    }
}
