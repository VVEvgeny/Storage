using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLTrackEditorTCX
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();






        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var m in _availableMapsGMapProvider)
            {
                comboBoxMapSelect.Items.Add(m.Key);
            }
            comboBoxMapSelect.Text = comboBoxMapSelect.Items[0].ToString();

            comboBoxMapSelect.SelectedIndexChanged += new System.EventHandler(comboBoxMapSelect_SelectedIndexChanged);

            //LoadGmap();
        }

        private void LoadGmap()
        {
            //Настройки для компонента GMap.
            gMapControl1.Bearing = 0;

            //CanDragMap - Если параметр установлен в True,
            //пользователь может перетаскивать карту
            ///с помощью правой кнопки мыши.
            gMapControl1.CanDragMap = true;

            //Указываем, что перетаскивание карты осуществляется
            //с использованием левой клавишей мыши.
            //По умолчанию - правая.
            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.GrayScaleMode = true;

            //MarkersEnabled - Если параметр установлен в True,
            //любые маркеры, заданные вручную будет показаны.
            //Если нет, они не появятся.
            gMapControl1.MarkersEnabled = true;

            //Указываем значение максимального приближения.
            gMapControl1.MaxZoom = 18;

            //Указываем значение минимального приближения.
            gMapControl1.MinZoom = 2;

            //Устанавливаем центр приближения/удаления
            //курсор мыши.
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            //Отказываемся от негативного режима.
            gMapControl1.NegativeMode = false;

            //Разрешаем полигоны.
            gMapControl1.PolygonsEnabled = true;

            //Разрешаем маршруты
            gMapControl1.RoutesEnabled = true;

            //Скрываем внешнюю сетку карты
            //с заголовками.
            gMapControl1.ShowTileGridLines = false;

            //Указываем, что при загрузке карты будет использоваться
            //18ти кратное приближение.
            gMapControl1.Zoom = 5;

            //Указываем что все края элемента управления
            //закрепляются у краев содержащего его элемента
            //управления(главной формы), а их размеры изменяются
            //соответствующим образом.
            //gMapControl1.Dock = DockStyle.Fill;

            //Указываем что будем использовать карты Google.
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMap.NET.MapProviders.GMapProvider.WebProxy = System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

        }

        private readonly Dictionary<string, GMap.NET.MapProviders.GMapProvider> _availableMapsGMapProvider =
            new Dictionary<string, GMap.NET.MapProviders.GMapProvider>()
            {
                {"Google", GMap.NET.MapProviders.GMapProviders.GoogleMap},
                {"Google Satellite", GMap.NET.MapProviders.GMapProviders.GoogleSatelliteMap},
                {"Google Hybrid", GMap.NET.MapProviders.GMapProviders.GoogleHybridMap},
                {"Bing", GMap.NET.MapProviders.GMapProviders.BingMap},
                //{"Yahoo", GMap.NET.MapProviders.GMapProviders.YahooMap},
                //{"Yahoo Satellite", GMap.NET.MapProviders.GMapProviders.YahooSatelliteMap},
                //{"Yahoo Hybrid", GMap.NET.MapProviders.GMapProviders.YahooHybridMap},
                //{"Open Street", GMap.NET.MapProviders.GMapProviders.OpenStreetMap},
                {"Yandex", GMap.NET.MapProviders.GMapProviders.YandexMap},
                {"Yandex Satellite", GMap.NET.MapProviders.GMapProviders.YandexSatelliteMap},
                {"Yandex Hybrid", GMap.NET.MapProviders.GMapProviders.YandexHybridMap}
            };

        private void comboBoxMapSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Указываем что будем использовать карты Google.
            gMapControl1.MapProvider = _availableMapsGMapProvider[comboBoxMapSelect.Text];
        }

        private List<TrackPoint> points = new List<TrackPoint>();

        private void ShowPoints()
        {
            listViewData.Items.Clear();

            for (int i = 0; i < points.Count; i++)
            {
                listViewData.Items.Add(new ListViewItem(new string[]
                {
                    i.ToString(), points[i].Time.ToString()
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            points.Clear();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\BM-CMP\\source\\repos\\VVEvgeny\\Storage\\XMLTrackEditorTCX\\data\\workout.tcx");

            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                //MessageBox.Show("root="+xnode.Name);
                foreach (XmlNode child in xnode.ChildNodes)
                {
                    //MessageBox.Show("child=" + child.Name);
                    foreach (XmlNode child2 in child.ChildNodes)
                    {
                        //MessageBox.Show("child2=" + child2.Name);
                        if (child2.Name == "Lap")
                        {
                            foreach (XmlNode lap in child2.ChildNodes)
                            {
                                if (lap.Name == "Track")
                                {
                                    foreach (XmlNode track in lap.ChildNodes)
                                    {
                                        if (track.Name == "Trackpoint")
                                        {
                                            var tp = new TrackPoint();
                                            foreach (XmlNode trackpoint in track.ChildNodes)
                                            {
                                                //MessageBox.Show(trackpoint.Name + "=" + trackpoint.InnerText);

                                                if (trackpoint.Name == "Time")
                                                {
                                                    tp.Time = Convert.ToDateTime(trackpoint.InnerText);
                                                }
                                                else if (trackpoint.Name == "Position")
                                                {
                                                    foreach (XmlNode position in trackpoint.ChildNodes)
                                                    {
                                                        if (position.Name == "LatitudeDegrees")
                                                        {
                                                            tp.Position.LatitudeDegrees = Convert.ToDouble(position.InnerText, CultureInfo.InvariantCulture);
                                                        }
                                                        else if (position.Name == "LongitudeDegrees")
                                                        {
                                                            tp.Position.LongitudeDegrees = Convert.ToDouble(position.InnerText, CultureInfo.InvariantCulture);
                                                        }
                                                    }
                                                }
                                                else if (trackpoint.Name == "AltitudeMeters")
                                                {
                                                    tp.AltitudeMeters = Convert.ToInt32(trackpoint.InnerText);
                                                }
                                                else if (trackpoint.Name == "HeartRateBpm")
                                                {
                                                    tp.HeartRateBpm = Convert.ToInt32(trackpoint.InnerText);
                                                }
                                            }

                                            points.Add(tp);

                                            if (points.Count > 100)
                                            {
                                                ShowPoints();
                                                return;
                                            }
                                            //MessageBox.Show(tp.ToString());

                                            //return;
                                        }
                                    }
                                }
                            }
                            //MessageBox.Show("points=" + points);
                        }
                    }
                }
            }

            ShowPoints();
        }

        private class TrackPoint
        {
            public DateTime Time;
            public PositionClass Position;
            public int AltitudeMeters;
            public int HeartRateBpm;

            public TrackPoint()
            {
                Position = new PositionClass();
            }

            public override string ToString()
            {
                var sb=new StringBuilder();

                sb.Append(nameof(Time) + "=" + Time + Environment.NewLine);
                sb.Append(nameof(Position.LatitudeDegrees) + "=" + Position.LatitudeDegrees + " " +
                          nameof(Position.LongitudeDegrees) + "=" + Position.LongitudeDegrees + Environment.NewLine);
                sb.Append(nameof(AltitudeMeters) + "=" + AltitudeMeters + Environment.NewLine);
                sb.Append(nameof(HeartRateBpm) + "=" + HeartRateBpm + Environment.NewLine);

                return sb.ToString();
            }

            public class PositionClass
            {
                public double LatitudeDegrees;
                public double LongitudeDegrees;
            }
        }

        private int selectedIndex = -1;
        private void listViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = sender as ListView;
            if (list != null)
            {
                if (list.SelectedIndices.Count > 0)
                {
                    selectedIndex = list.SelectedIndices[0];
                    var item = points[selectedIndex];

                    textBoxTime.Text = item.Time.ToString(CultureInfo.InvariantCulture);
                    textBoxLatitude.Text = item.Position.LatitudeDegrees.ToString(CultureInfo.InvariantCulture);
                    textBoxLongitude.Text = item.Position.LongitudeDegrees.ToString(CultureInfo.InvariantCulture);


                    textBoxAltitude.Text = item.AltitudeMeters.ToString();
                    textBoxHeartRate.Text = item.HeartRateBpm.ToString();

                    return;
                }
            }

            selectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                var item = points[selectedIndex];

                item.Time = Convert.ToDateTime(textBoxTime.Text, CultureInfo.InvariantCulture);
                item.Position.LatitudeDegrees = Convert.ToDouble(textBoxLatitude.Text, CultureInfo.InvariantCulture);
                item.Position.LongitudeDegrees = Convert.ToDouble(textBoxLongitude.Text, CultureInfo.InvariantCulture);
                item.AltitudeMeters = Convert.ToInt32(textBoxAltitude.Text);
                item.HeartRateBpm = Convert.ToInt32(textBoxHeartRate.Text);

                listViewData.Items[selectedIndex] = new ListViewItem(new string[]
                {
                    selectedIndex.ToString(), item.Time.ToString()
                });

            }
        }
    }
}
