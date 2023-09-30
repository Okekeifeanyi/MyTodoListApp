using System;
using System.Linq;
using System.Windows.Forms;


namespace ToDoListEFC
{
    public partial class ToDoList : Form
    {
        Entities entities = new Entities();

        public ToDoList()
        {
            InitializeComponent();
        }

        IfeanyiContext db = new IfeanyiContext(); // Create a context instance
        bool isEditing = false;

        private void Form1_Load(object sender, EventArgs e)
        {

            // Load tasks from the database and bind them to the DataGridView
            RefreshTaskList();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            taskButton.Text = "";
            descriptionButton.Text = "";
            // Open a new form for adding a new task and save it to the database
            // You can create a new form to input task details and handle the database operation there
            // Once the task is saved, call RefreshTaskList() to update the DataGridView
        }

        //private void saveButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string taskdescription = taskButton.Text.Trim();
        //        string descriptiondescription = descriptionButton.Text.Trim();
        //        if (!string.IsNullOrEmpty(taskdescription) && !string.IsNullOrEmpty(descriptiondescription))
        //        {
        //            var newitem = new Entities()
        //            {
        //                Task = taskdescription,
        //                Description = descriptiondescription,
        //                Created_Date = DateTime.Now,
        //                Updated_Date = DateTime.Now,
        //                // Deleted = fasle
        //            };
        //            db.Entity.Add(newitem);
        //            db.SaveChanges();

        //            // Refresh the task list in the DataGridView
        //            RefreshTaskList();

        //            // Clear the input fields
        //            taskButton.Text = "";
        //            descriptionButton.Text = "";

        //        }
        //        else
        //        {
        //            MessageBox.Show("Task and description must not be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleError(ex);
        //    }
        

        //    // Update the existing task in the database
        //    // Retrieve the task to be edited, apply changes, and save it
        //    // Create a new task in the database
        //    // Create a new task object, set its properties, and add it to the database

        //}

        private void HandleError(Exception ex)
        {
            MessageBox.Show($"An error occured: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Entities selectedTask; // Declare a field to store the selected task for editing

        private void editButton_Click(object sender, EventArgs e)
        {
            if (toDoListGridView.SelectedRows.Count > 0)
            {
                // Get the selected task from the DataGridView
                selectedTask = toDoListGridView.SelectedRows[0].DataBoundItem as Entities;

                // Display the selected task's details for editing
                taskButton.Text = selectedTask.Task;
                descriptionButton.Text = selectedTask.Description;

                // Set the isEditing flag to true to indicate that we are in edit mode
                isEditing = true;
            }
            else
            {
                MessageBox.Show("Please select a task to edit.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string taskdescription = taskButton.Text.Trim();
                string descriptiondescription = descriptionButton.Text.Trim();

                if (!string.IsNullOrEmpty(taskdescription) && !string.IsNullOrEmpty(descriptiondescription))
                {
                    if (isEditing)
                    {
                        // If in edit mode, update the selected task in the database
                        selectedTask.Task = taskdescription;
                        selectedTask.Description = descriptiondescription;
                        selectedTask.Updated_Date = DateTime.Now;

                        db.SaveChanges();

                        // Reset the isEditing flag
                        isEditing = false;
                    }
                    else
                    {
                        // If not in edit mode, create a new task in the database
                        var newitem = new Entities()
                        {
                            Task = taskdescription,
                            Description = descriptiondescription,
                            Created_Date = DateTime.Now,
                            Updated_Date = DateTime.Now,
                        };

                        db.Entity.Add(newitem);
                        db.SaveChanges();
                    }

                    // Refresh the task list in the DataGridView
                    RefreshTaskList();

                    // Clear the input fields
                    taskButton.Text = "";
                    descriptionButton.Text = "";
                }
                else
                {
                    MessageBox.Show("Task and description must not be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (toDoListGridView.SelectedRows.Count > 0)
            {
                // Get the selected task from the DataGridView
                var selectedTask = toDoListGridView.SelectedRows[0].DataBoundItem as Entities;

                // Remove the selected task from the database
                db.Entity.Remove(selectedTask);
                db.SaveChanges();

                // Refresh the task list
                RefreshTaskList();
            }
            // Check if a task is selected in the DataGridView
            // If yes, delete it from the database and then call RefreshTaskList()
        }

        private void deleteAllButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete all items?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Remove all tasks from the database
                    var allTasks = db.Entity.ToList();
                    db.Entity.RemoveRange(allTasks);
                    db.SaveChanges();

                    // Refresh the task list
                    RefreshTaskList();

                    MessageBox.Show("All items have been deleted.", "Deletion Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchKeyword = searchBox.Text.Trim();

            // Query the database to filter tasks based on the searchKeyword
            var filteredTasks = db.Entity
                .Where(task => task.Task.Contains(searchKeyword) || task.Description.Contains(searchKeyword))
                .ToList();

            // Bind the filtered tasks to the DataGridView
            toDoListGridView.DataSource = filteredTasks;
        }


        private void RefreshTaskList()
        {
           
                // Load tasks from the database and bind them to the DataGridView
                toDoListGridView.DataSource = db.Entity.ToList();
            

            // Load tasks from the database and bind them to the DataGridView
            // Use LINQ to Entities to query the database and populate the DataGridView
        }


    }
}
