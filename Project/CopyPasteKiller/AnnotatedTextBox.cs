using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace CopyPasteKiller
{
	public class AnnotatedTextBox : UserControl, IComponentConnector, IStyleConnector
	{
		private AnnotatedTextViewModel _annotatedTextViewModel = new AnnotatedTextViewModel();

		public static readonly DependencyProperty CodeFileProperty;

		public static readonly DependencyProperty SimilarityProperty;

		private EventHandler<SimilaritySelectedEventArgs> _similaritySelectedEventHandler;

		internal Grid grid;

		internal TextBox textBox1;

		internal TextBox textBox2;

		private bool _isInitialized;

		public event EventHandler<SimilaritySelectedEventArgs> SimilaritySelected
		{
			add
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = _similaritySelectedEventHandler;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref _similaritySelectedEventHandler, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<SimilaritySelectedEventArgs> eventHandler = _similaritySelectedEventHandler;
				EventHandler<SimilaritySelectedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimilaritySelectedEventArgs> value2 = (EventHandler<SimilaritySelectedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimilaritySelectedEventArgs>>(ref _similaritySelectedEventHandler, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public CodeFile CodeFile
		{
			get
			{
				return (CodeFile)base.GetValue(AnnotatedTextBox.CodeFileProperty);
			}
			set
			{
				base.SetValue(AnnotatedTextBox.CodeFileProperty, value);
			}
		}

		public Similarity Similarity
		{
			get
			{
				return (Similarity)base.GetValue(AnnotatedTextBox.SimilarityProperty);
			}
			set
			{
				base.SetValue(AnnotatedTextBox.SimilarityProperty, value);
			}
		}

		public AnnotatedTextBox()
		{
			InitializeComponent();
			grid.DataContext = _annotatedTextViewModel;
		}

		private static object smethod0(DependencyObject dependencyObject, CodeFile codeFile)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject as AnnotatedTextBox;
			object result;

			if (annotatedTextBox != null)
			{
				result = annotatedTextBox.OnCoerceCodeFile((CodeFile)codeFile);
			}
			else
			{
				result = codeFile;
			}

			return result;
		}

		private static void smethod1(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject as AnnotatedTextBox;

			if (annotatedTextBox != null)
			{
				annotatedTextBox.OnCodeFileChanged((CodeFile)eventArgs.OldValue, (CodeFile)eventArgs.NewValue);
			}
		}

		protected virtual CodeFile OnCoerceCodeFile(CodeFile codeFile)
		{
			return codeFile;
		}

		protected virtual void OnCodeFileChanged(CodeFile oldValue, CodeFile newValue)
		{
			_annotatedTextViewModel.CodeFile = newValue;
		}

		private static object smethod2(DependencyObject dependencyObject, Similarity similarity)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject as AnnotatedTextBox;
			object result;

			if (annotatedTextBox != null)
			{
				result = annotatedTextBox.OnCoerceSimilarity((Similarity)similarity);
			}
			else
			{
				result = similarity;
			}

			return result;
		}

		private static void smethod3(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			AnnotatedTextBox annotatedTextBox = dependencyObject as AnnotatedTextBox;

			if (annotatedTextBox != null)
			{
				annotatedTextBox.OnSimilarityChanged((Similarity)eventArgs.OldValue, (Similarity)eventArgs.NewValue);
			}
		}

		protected virtual Similarity OnCoerceSimilarity(Similarity similarity)
		{
			if (similarity == null)
			{
				method1(0);
			}
			else
			{
				method1(similarity.MyRange.Start);
			}

			return similarity;
		}

		protected virtual void OnSimilarityChanged(Similarity oldValue, Similarity newValue)
		{
		}

		private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement frameworkElement = (FrameworkElement)sender;
			Annotation annotation = (Annotation)frameworkElement.DataContext;

			if (_similaritySelectedEventHandler != null)
			{
				_similaritySelectedEventHandler(this, new SimilaritySelectedEventArgs
				{
					Similarity = annotation.Similarity.CorrespondingSimilarity
				});
			}
		}

		internal void method1(int int0)
		{
			try
			{
				textBox2.ScrollToVerticalOffset((double)int0 * Annotation.TextHeight);
			}
			catch (Exception)
			{
			}
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Uri resourceLocator = new Uri("/Atomiq;component/annotatedtextbox.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
				case 1:
					grid = (Grid)target;
					return;
				case 3:
					textBox1 = (TextBox)target;
					return;
				case 4:
					textBox2 = (TextBox)target;
					return;
			}

			_isInitialized = true;
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((Rectangle)target).MouseDown += Rectangle_MouseDown;
			}
		}

		static AnnotatedTextBox()
		{
			//AnnotatedTextBox.CodeFileProperty = DependencyProperty.Register("CodeFile", typeof(CodeFile), typeof(AnnotatedTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(AnnotatedTextBox.smethod1), new CoerceValueCallback(AnnotatedTextBox.smethod0)));
			//AnnotatedTextBox.SimilarityProperty = DependencyProperty.Register("Similarity", typeof(Similarity), typeof(AnnotatedTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(AnnotatedTextBox.smethod3), new CoerceValueCallback(AnnotatedTextBox.smethod2)));
		}
	}
}