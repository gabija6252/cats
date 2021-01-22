using System;
using MyLibrary;
using System.Drawing;
using System.Windows.Forms;

namespace cats
{
    public class Form1 : Form
    {
        [STAThread]
        static void Main(String[] args)
        {
            //API api = new API();
            //api.getCats("beng");
            //api.getBreeds();
            //api.getImage("https://cdn2.thecatapi.com/images/ozEvzdVM-.jpg");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
                   
        }


        public Button button1;
        public Button button2;
        public ComboBox combobox;
        public Label name;

        public Form1()
        {
            //programos langas
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cats";
            this.ResumeLayout(false);

            //label
            name = new Label();
            name.Location = new Point((this.ClientSize.Width - name.Width - 80) / 2, (this.ClientSize.Height - name.Height - 200) / 2);
            name.Text = "Random Cats";
            name.Font = new Font("Calibri", 20, FontStyle.Bold);
            name.AutoSize = true;

            //button1
            button1 = new Button();
            button1.Size = new Size(100, 50);
            button1.Location = new Point((this.ClientSize.Width - button1.Width - 150) / 2, (this.ClientSize.Height - button1.Height + 200) / 2);
            button1.Text = "Random Photo";
            button1.Click += new EventHandler(button1_Click);
            button1.Anchor = AnchorStyles.None;

            //button2
            button2 = new Button();
            button2.Size = new Size(100, 50);
            button2.Location = new Point((this.ClientSize.Width - button2.Width + 150) / 2, (this.ClientSize.Height - button2.Height + 200) / 2);
            button2.Text = "INFO";
            button2.Click += new EventHandler(button2_Click);
            button2.Anchor = AnchorStyles.None;

            //combobox
            combobox = new ComboBox();
            combobox.Size = new Size(245, 40);
            combobox.Location = new Point((this.ClientSize.Width - combobox.Width) / 2, (this.ClientSize.Height - combobox.Height + 50) / 2);
            combobox.Name = "combobox";
            combobox.BackColor = System.Drawing.Color.White;
            combobox.ForeColor = System.Drawing.Color.Black;

            API api = new API();
            Breed[] breeds = api.getBreeds();

            for (int i = 0; i < breeds.Length; i++)
            {
                combobox.Items.Add(breeds[i].name);
            }

            this.Controls.Add(button1);
            this.Controls.Add(button2);
            this.Controls.Add(combobox);
            this.Controls.Add(name);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Photo(combobox.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Info(combobox.Text));
        }
    }


    public class Photo : Form
    {
        public PictureBox picture;
        public String id;
        public Button button;

        public Photo(String brd_name)
        {
            //button
            button = new Button();
            button.Size = new Size(100, 35);
            button.Location = new Point(10, 10);
            button.Text = "Close";
            button.Click += new EventHandler(button_Click);
            button.Anchor = AnchorStyles.None;

            picture = new PictureBox();

            API api = new API();
            Breed[] breeds = api.getBreeds();


            for (int i = 0; i < breeds.Length; i++)
            {
                if(breeds[i].name == brd_name)
                {
                    id = breeds[i].id;
                }
            }

            Cat[] cat = api.getCats(id);

            //picture.SizeMode = PictureBoxSizeMode.StretchImage; 
            //picture.Size = new Size(cat[0].width, cat[0].height);
            picture.Location = new Point(50, 50);
           

            if (cat[0].width > 1400 && cat[0].height > 900)
            {
                picture.Size = new Size(1400, 900);
                this.Size = new Size(1500, 1000);
                picture.SizeMode = PictureBoxSizeMode.Zoom;
            }else if (cat[0].width > 1400)
            {
                picture.Size = new Size(1400, cat[0].height);
                this.Size = new Size(1500, cat[0].height + 100);
            }
            else if (cat[0].height > 900)
            {
                picture.Size = new Size(cat[0].width, 900);
                this.Size = new Size(cat[0].width + 100, 1000);
            }
            else
            {
                picture.Size = new Size(cat[0].width, cat[0].height);
                this.Size = new Size(cat[0].width + 100, cat[0].height + 100);
            }

            picture.BackgroundImage = Image.FromStream(api.getImage(cat[0].url));


            //programos langas
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cute Photo";
            //this.Size = new Size(cat[0].width + 100, cat[0].height + 100);
            this.ResumeLayout(false);
            this.Controls.Add(picture);
            this.Controls.Add(button);

        }
        private void button_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

    }

    public class Info : Form
    {
        public Button button;
        public int sk = -1;
        public Label info;
        public Label name;
        public Label temperament;
        public Label life_span;
        public Label alt_name;
        public Label origin;

        public Info(String brd_name)
        {
            API api = new API();
            Breed[] breeds = api.getBreeds();

            //button
            button = new Button();
            button.Size = new Size(100, 35);
            button.Location = new Point(10, 10);
            button.Text = "Close";
            button.Click += new EventHandler(button_Click);
            button.Anchor = AnchorStyles.None;

            //label info
            info = new Label();
            info.Location = new Point(365, 10);
            info.Text = "INFO";
            info.Font = new Font("Calibri", 20, FontStyle.Bold);
            info.AutoSize = true;


            for (int i = 0; i < breeds.Length; i++)
            {
                if (breeds[i].name == brd_name)
                {
                    sk = i;
                }
            }

            if (sk != -1)
            {
                //label name
                name = new Label();
                name.Location = new Point(10, 90);
                name.Text = "Name: " + breeds[sk].name;
                name.Font = new Font("Calibri", 12, FontStyle.Bold);
                name.AutoSize = true;

                //label temperament
                temperament = new Label();
                temperament.Location = new Point(10, 120);
                temperament.Text = "Temperament: " + breeds[sk].temperament;
                temperament.Font = new Font("Calibri", 12, FontStyle.Bold);
                temperament.AutoSize = true;

                //label life_span
                life_span = new Label();
                life_span.Location = new Point(10, 150);
                life_span.Text = "Life Span: " + breeds[sk].life_span;
                life_span.Font = new Font("Calibri", 12, FontStyle.Bold);
                life_span.AutoSize = true;

                //label alt_names
                alt_name = new Label();
                alt_name.Location = new Point(10, 180);
                alt_name.Text = "Alt Name: " + breeds[sk].alt_names;
                alt_name.Font = new Font("Calibri", 12, FontStyle.Bold);
                alt_name.AutoSize = true;

                //label origin
                origin = new Label();
                origin.Location = new Point(10, 210);
                origin.Text = "Origin: " + breeds[sk].origin;
                origin.Font = new Font("Calibri", 12, FontStyle.Bold);
                origin.AutoSize = true;
            }
            else
            {
                //label name
                name = new Label();
                name.Location = new Point(10, 90);
                name.Text = "Name: ";
                name.Font = new Font("Calibri", 12, FontStyle.Bold);
                name.AutoSize = true;

                //label temperament
                temperament = new Label();
                temperament.Location = new Point(10, 120);
                temperament.Text = "Temperament: ";
                temperament.Font = new Font("Calibri", 12, FontStyle.Bold);
                temperament.AutoSize = true;

                //label life_span
                life_span = new Label();
                life_span.Location = new Point(10, 150);
                life_span.Text = "Life Span: ";
                life_span.Font = new Font("Calibri", 12, FontStyle.Bold);
                life_span.AutoSize = true;

                //label alt_names
                alt_name = new Label();
                alt_name.Location = new Point(10, 180);
                alt_name.Text = "Alt Name: ";
                alt_name.Font = new Font("Calibri", 12, FontStyle.Bold);
                alt_name.AutoSize = true;

                //label origin
                origin = new Label();
                origin.Location = new Point(10, 210);
                origin.Text = "Origin: ";
                origin.Font = new Font("Calibri", 12, FontStyle.Bold);
                origin.AutoSize = true;
            }
            //programos langas
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INFO";
            this.Size = new Size(800, 300);
            this.ResumeLayout(false);

            this.Controls.Add(button);
            this.Controls.Add(info);
            this.Controls.Add(name);
            this.Controls.Add(temperament);
            this.Controls.Add(life_span);
            this.Controls.Add(alt_name);
            this.Controls.Add(origin);

        }
        private void button_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

    }
}


