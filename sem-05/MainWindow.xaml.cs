using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Windows.Automation;
using System.Windows.Media.Media3D;
using System;

namespace sem_05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void botonListaCliente_Click(object sender, RoutedEventArgs e)
        {
            ListData();
        }

        private void botonActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            ActualizarData();
        }

        //Listar datos
        private void ListData(string filter = "")
        {
            try
            {
                //"Data Source=DESKTOP-F970KVM;Initial Catalog=FacturaDB;User ID=usrFactura;Password=123456"
                //string cadena = "Data Source=RED\\SQLEXPRESS;Initial Catalog=Neptuno2;User ID=userNeptuno;Password=123456";
                string cadena = "Server=RED\\SQLEXPRESS; Database=Neptuno2; Integrated Security=True; User ID=userNeptuno;Password=123456";
                using SqlConnection conect = new SqlConnection(cadena);

                conect.Open();

                SqlCommand comand = new SqlCommand("USP_ListarClientes", conect);
                comand.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = comand.ExecuteReader();
                List<Cliente> listaClientes = new List<Cliente>();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        idCliente = reader["idCliente"].ToString(),
                        NombreCompañia = reader["NombreCompañia"].ToString(),
                        NombreContacto = reader["NombreContacto"].ToString(),
                        CargoContacto = reader["CargoContacto"].ToString(),
                        Ciudad = reader["Ciudad"].ToString()
                    };
                    listaClientes.Add(cliente);
                }


                listadoClientes.ItemsSource = listaClientes;

                conect.Close();

                //listadoClientes.DataSource = listaClientes;

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al cargar los clientes: {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Actualizar datos
        private void ActualizarData(string filter = "")
        {
            try
            {
                string cadena = "Server=RED\\SQLEXPRESS; Database=Neptuno2; Integrated Security=True; User ID=userNeptuno;Password=123456";
                using SqlConnection conect = new SqlConnection(cadena);

                conect.Open();

                SqlCommand comand = new SqlCommand("USP_ActualizarClientes", conect);
                comand.CommandType = CommandType.StoredProcedure;

                comand.Parameters.AddWithValue("@idCliente", txtIdCliente.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@NombreCompañia", txtNombreCompañia.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@CargoContacto", txtCargoContacto.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@Direccion", txtDireccion.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@Ciudad", txtCiudad.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@Region", txtRegion.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@CodPostal", txtCodPostal.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@Pais", txtPais.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@Telefono", txtTelefono.Text ?? (object)DBNull.Value);
                comand.Parameters.AddWithValue("@Fax", txtFax.Text ?? (object)DBNull.Value);

                comand.ExecuteNonQuery();

                MessageBox.Show("Cliente actualizado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                conect.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al actualizar el cliente: {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        
    }
}