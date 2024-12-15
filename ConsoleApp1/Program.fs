open System
open System.Windows.Forms
open System.Drawing

// Contact type to hold contact details
type Contact = {
    Name: string
    PhoneNumber: string
    Email: string
}

// Main Form 
type ContactForm() as this =
    inherit Form()

    
    let mutable contacts = Map.empty<string, Contact>
   
    let mutable selectedContact: Contact option = None

    // UI 
    let listBox = new ListBox(Dock = DockStyle.Left, Width = 250, BackColor = Color.LightGray, ForeColor = Color.Black, Font = new Font("Arial", 10f))
    let nameTextBox = new TextBox(Dock = DockStyle.Top, PlaceholderText = "Name", Height = 40, Font = new Font("Arial", 10f))
    let phoneTextBox = new TextBox(Dock = DockStyle.Top, PlaceholderText = "Phone Number", Height = 40, Font = new Font("Arial", 10f))
    let emailTextBox = new TextBox(Dock = DockStyle.Top, PlaceholderText = "Email", Height = 40, Font = new Font("Arial", 10f))
    let addButton = new Button(Text = "Save", Dock = DockStyle.Top, Height = 50, Font = new Font("Arial", 12f, FontStyle.Bold), BackColor = Color.Teal, ForeColor = Color.White)
    let updateButton = new Button(Text = "Update", Dock = DockStyle.Top, Height = 50, Font = new Font("Arial", 12f, FontStyle.Bold), BackColor = Color.Orange, ForeColor = Color.White)
    let searchTextBox = new TextBox(Dock = DockStyle.Top, PlaceholderText = "Search", Height = 40, Font = new Font("Arial", 10f))
    let searchButton = new Button(Text = "Search", Dock = DockStyle.Top, Height = 50, Font = new Font("Arial", 12f, FontStyle.Bold), BackColor = Color.CadetBlue, ForeColor = Color.White)
    let deleteButton = new Button(Text = "Delete", Dock = DockStyle.Top, Height = 50, Font = new Font("Arial", 12f, FontStyle.Bold), BackColor = Color.Crimson, ForeColor = Color.White)

   
    do
        this.Text <- "Contact Management System"
        this.ClientSize <- System.Drawing.Size(900, 450)
        this.BackColor <- Color.WhiteSmoke

        
        this.Controls.AddRange([| deleteButton; updateButton; addButton; emailTextBox; phoneTextBox; nameTextBox; searchButton; searchTextBox; listBox |])

       
        addButton.Click.Add(fun _ -> this.AddContactHandler())
        updateButton.Click.Add(fun _ -> this.UpdateContactHandler())
        deleteButton.Click.Add(fun _ -> this.DeleteContact())
        searchButton.Click.Add(fun _ -> this.SearchContact())
        listBox.SelectedIndexChanged.Add(fun _ -> this.SelectContactForEditing())
