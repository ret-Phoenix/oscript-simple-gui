using System;
using System.Windows.Forms;

namespace TreeViewColumnsProject
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			TreeNode treeNode = new TreeNode("test");
			treeNode.Tag = new string[] { "col1", "col2", "col3" };

            //// Some random node
            //this.treeViewColumns1.TreeView.Nodes[0].Nodes[0].Nodes.Add(treeNode);

            //this.treeViewColumns1.TreeView.SelectedNode = treeNode;

            this.treeViewColumns1.Columns.Add("МояКолонка");
            this.treeViewColumns1.TreeView.FullRowSelect = false;

        }
	}
}