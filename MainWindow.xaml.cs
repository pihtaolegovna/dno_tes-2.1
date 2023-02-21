using dno_tes_2._1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace WindowChromeExample
{
    public partial class MainWindow : Window
    {

        public static BindingList<Note> Notes = new BindingList<Note>();
        public static BindingList<String> Notesname = new BindingList<String>();
        public static BindingList<String> NotesText = new BindingList<String>();

        public MainWindow()
        {
            InitializeComponent();

            Notes.Add(new Note("dasxfarwerfef4rweg3efrefdsagreargetrujheytbgvfdgsedssxst5ewr", "fasfdewdqhfjewqia;ejdieo;\newfqqfeeqwqf342qdewfd2huilehuiqfhquqeilhfeuilfhweuihfauhfwilad", DateTime.Now));
            Notes.Add(new Note("dfxfa", "fafsdfd", DateTime.Now));
            Notes.Add(new Note("dfxfaf", "fafsdd", DateTime.Now));

            foreach (var note in Notes)
            {
                Notesname.Add(note.NoteName);
                NotesText.Add(note.NoteText);
            }

            NotesListBox.ItemsSource = Notesname;
        }

        public void update()
        {
            NotesListBox.ItemsSource = Notesname;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void MainWindowStateChangeRaised(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.BorderThickness = new Thickness(8);
                RestoreButton.Visibility = Visibility.Visible;
                MaximizeButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainWindowBorder.BorderThickness = new Thickness(0);
                RestoreButton.Visibility = Visibility.Collapsed;
                MaximizeButton.Visibility = Visibility.Visible;
            }
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NoteFieldTextBox.Text = NotesText[NotesListBox.SelectedIndex];
            NameTextBox.Text = Notesname[NotesListBox.SelectedIndex];
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!(NameTextBox.Text == ""))
            {
                Notes.Add(new Note(NameTextBox.Text, NoteFieldTextBox.Text, DateTime.Now));
                Notesname.Add(NameTextBox.Text);
                update();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Notes.RemoveAt(NotesListBox.SelectedIndex);
            Notesname.RemoveAt(NotesListBox.SelectedIndex);
            NotesText.RemoveAt(NotesListBox.SelectedIndex);
            update();
        }
    }
}
