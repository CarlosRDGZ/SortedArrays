using Gtk;
using System;
using SortedArray;

public partial class MainWindow : Gtk.Window
{
	private readonly Catalog _catalog = new Catalog(15);

    public MainWindow() : base(Gtk.WindowType.Toplevel) => Build();

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

	private void ShowMessageBox(string message, MessageType type)
	{
		ButtonsType buttons = type == MessageType.Error ? ButtonsType.Close : ButtonsType.Ok;
		MessageDialog msg = new MessageDialog(
			this,
			DialogFlags.Modal,
			type,
			buttons,
			message
		);
		msg.Run();
		msg.Destroy();
	}

	protected void BtnAddClick(object sender, EventArgs e)
	{
		string[] props = {
			txtName.Text,
			txtDescription.Text,
			txtQuantity.Text,
			txtPrice.Text,
		};
		int[] nums = new int[2];
		if (props[0] != null && props[1] != null &&
			int.TryParse(props[2], out nums[0]) && int.TryParse(props[2], out nums[1]))
		{
			_catalog.Add(new Product(props[0], props[1], nums[0], nums[1]));

			ShowMessageBox("Producto Agregado", MessageType.Info);
			txtName.Text = "";
			txtDescription.Text = "";
			txtQuantity.Text = "";
			txtPrice.Text = "";
		}
		else
			ShowMessageBox("ERROR!", MessageType.Error);
	}

	protected void BtnListClick(object sender, EventArgs e) => txtList.Buffer.Text = _catalog.List();

	protected void BtnSearchClick(object sender, EventArgs e)
	{
		Product product = _catalog.Search(txtCode.Text);
		if (product != null)
		{
			txtList.Buffer.Text = product.ToJSON();
			txtCode.Text = "";         
        }      
		else
			ShowMessageBox("ERROR! Producto No Econtrado.", MessageType.Error);
	}

	protected void BtnDeleteClick(object sender, EventArgs e)
	{
		try
		{
			_catalog.Delete(txtCode.Text);
			ShowMessageBox("Producto Eliminado", MessageType.Info);
			txtCode.Text = "";    
		}
		catch(Exception)
		{
			ShowMessageBox("ERROR!. Producto No Econtrado.", MessageType.Error);
		}      
	}
}
