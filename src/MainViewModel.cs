// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace SyntaxHighlighting
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel()
		{
			Document = new TextDocument();
			Document.Text = 
				"<span>\r\n" +
				"@Html.Raw(model.Message)\r\n" +
				"</span>\r\n" +
				"{\r\n" +
				"\tfoo: bar;\r\n" +
				"}\r\n";
			
			SyntaxHighlightingFileName = 
				@"D:\projects\dotnet\mirador\src\Libraries\AvalonEdit\ICSharpCode.AvalonEdit\Highlighting\Resources\PowerShell.xshd";
			SyntaxHighlightingName = "C#";
			
			//ReloadSyntaxHighlighting();
			ReloadCommand = new DelegateCommand(param => ReloadSyntaxHighlighting());
		}
		
		public ICommand ReloadCommand { get; private set; }
		public string SyntaxHighlightingFileName { get; set; }
		public TextDocument Document { get; set; }
		
		string syntaxHighlightingName;
		
		public string SyntaxHighlightingName {
			get { return syntaxHighlightingName; }
			set { syntaxHighlightingName = value; }
		}
		
		public IHighlightingDefinition Highlighting { get; set; }
		
		void ReloadSyntaxHighlighting()
		{
			try {
				XshdSyntaxDefinition xshd;
				using (XmlTextReader reader = new XmlTextReader(SyntaxHighlightingFileName)) {
					xshd = HighlightingLoader.LoadXshd(reader);
				}
				Highlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
				HighlightingManager.Instance.RegisterHighlighting("VBNET/Razor", new string[] { ".ps1" }, Highlighting);
				SyntaxHighlightingName = "VBNET/Razor";
				
				OnPropertyChanged(null);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(null));
			}
		}
	}
}
