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
    member private this.AddContact(name: string, phone: string, email: string) =
        if contacts.ContainsKey(phone) then
            MessageBox.Show("A contact with this phone number already exists.") |> ignore
        else
            let contact = { Name = name; PhoneNumber = phone; Email = email }
            contacts <- contacts.Add(phone, contact)

    member private this.EditContact(name: string, phone: string, email: string) =
        match selectedContact with
        | Some existingContact ->
            // Remove the old contact and add the updated one
            contacts <- contacts.Remove(existingContact.PhoneNumber)
            let updatedContact = { Name = name; PhoneNumber = phone; Email = email }
            contacts <- contacts.Add(phone, updatedContact)
        | None ->
            MessageBox.Show("No contact selected for editing.") |> ignore
// ...
    member private this.UpdateListBox() =
        listBox.Items.Clear()
        contacts |> Map.iter (fun _ contact -> listBox.Items.Add(sprintf "%s - %s" contact.Name contact.PhoneNumber) |> ignore)

   
    member private this.AddContactHandler() =
        let name = nameTextBox.Text
        let phone = phoneTextBox.Text
        let email = emailTextBox.Text
        if name <> "" && phone <> "" then
            this.AddContact(name, phone, email)
            this.UpdateListBox()
            this.ClearFields()
        else
            MessageBox.Show("Please fill in the name and phone number.") |> ignore
    member private this.UpdateContactHandler() =
        let name = nameTextBox.Text
        let phone = phoneTextBox.Text
        let email = emailTextBox.Text
        if selectedContact.IsNone then
            MessageBox.Show("No contact selected for editing.") |> ignore
        elif name <> "" && phone <> "" then
            this.EditContact(name, phone, email)
            this.UpdateListBox()
            this.ClearFields()
        else
            MessageBox.Show("Please fill in the name and phone number.") |> ignore

