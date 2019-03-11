using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using gMapControls;


namespace gmap_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        GMapRoute groute;
        List<GMapMarker> points_route = new List<GMapMarker>();
        Image[] m_Img = { Properties.Resources.Image_plane1, Properties.Resources.Image_target, Properties.Resources.point_polygon, Properties.Resources.point_route };
        private GMapOverlay Overlay_Route;
        private GMapOverlay Overlay_Poly;

        public GMap.NET.MapProviders.GMapProvider gMapProviders = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;


        double cul_lat=31.929073, cul_lng=118.769073;

        private GMapOverlay overlay_draw;
        private GMapOverlay overlay_plane;



        private void Form1_Load(object sender, EventArgs e)
        {
            overlay_draw = new GMapOverlay(this.gMapControl1, "MakersOverLay");
            Overlay_Poly = new GMapOverlay(this.gMapControl1, "PolyOverlay");
            overlay_plane = new GMapOverlay(this.gMapControl1, "PlaneOverlay");
            this.gMapControl1.Overlays.Add(overlay_draw);
            this.gMapControl1.Overlays.Add(Overlay_Poly);
            this.gMapControl1.Overlays.Add(overlay_plane);

            //gMapControl1.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;

            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //gMapControl1.MapProvider = AmapProvide.Instance;
            gMapControl1.MapProvider = AmapProvide.Instance;
            
            
            
            
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            //gMapControl1.SetCurrentPositionByKeywords("Maputo,Mozambique");
            this.gMapControl1.Position = new GMap.NET.PointLatLng(31.938074, 118.789600);
            this.gMapControl1.Zoom = 14;

            Overlay_Route = new GMapOverlay(this.gMapControl1, "RouteOverlay");
            this.gMapControl1.Overlays.Add(Overlay_Route);

            Init_Controls(3);
            
        }

        private void Init_Controls(int index)
        {
            this.comboBox1.Items.Add("BingHybridMap");
            this.comboBox1.Items.Add("BingMapOld");
            this.comboBox1.Items.Add("BingMap");
            this.comboBox1.Items.Add("BingSatelliteMap");
            this.comboBox1.Items.Add("OpenCycleMap");
            this.comboBox1.Items.Add("OpenSeaMapHybrid");
            this.comboBox1.Items.Add("OpenStreetMap");
            this.comboBox1.Items.Add("BingMap");
            this.comboBox1.Items.Add("GaoDeMap");

            this.comboBox1.SelectedIndex = index;
            selectedIndex = index;
        }

        bool check_box = true;
        private void button1_Click(object sender, EventArgs e)
        {
            PointLatLng curPoint = new PointLatLng(31.938074, 118.789600);
            GMapOverlay markersoverlay = new GMapOverlay(this.gMapControl1,"markers");
            this.gMapControl1.Overlays.Add(markersoverlay);
            //markersoverlay.Markers.Add(new GMapMarkerGoogleGreen(curPoint));

            if (check_box == true)
            {
                this.timer1.Start();
                check_box = false;
                this.button1.BackColor = Color.Blue;
            }
            else
            {
                this.timer1.Stop();
                check_box = true;
                this.button1.BackColor = Color.DarkGray;
            }
            //curPoint.Lat = 31.935074;
            //curPoint.Lng = 118.785600;
            //markers_route = new GMapMarkerImage(curPoint, m_Img[3]);
            //markersoverlay.Markers.Add(markers_route);
        }


        private GPoint LastGP = new GPoint();
        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng LatLng = this.gMapControl1.FromLocalToLatLng(e.X, e.Y);
            this.label1.Text = string.Format("经度: {0}\n纬度: {1}", LatLng.Lng.ToString("0.000000"), LatLng.Lat.ToString("0.000000"));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GMapOverlay polyOverlay = new GMapOverlay(this.gMapControl1, "polygons");
            List<PointLatLng> Points = new List<PointLatLng>();
            Points.Add(new PointLatLng(31.938074, 118.789600));
            Points.Add(new PointLatLng(31.934074, 118.789600));
            Points.Add(new PointLatLng(31.934074, 118.785600));
            Points.Add(new PointLatLng(31.938074, 118.785600));
            GMapPolygon polygon = new GMapPolygon(Points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);
            gMapControl1.Overlays.Add(polyOverlay);
        }

        int index = 1;
        GMapMarker markers_now,markers_next;
        List<PointLatLng> points1;
        int j = 0;
        GMapMarker plane_route;
        private void timer1_Tick(object sender, EventArgs e)
        {
            double last_lat, last_lng;
            //Image Img_plane,img_plane1;
            
            last_lat = cul_lat - 0.003;
            last_lng = cul_lng - 0.005;
            j++;
            points1 = new List<PointLatLng>();
            PointLatLng now_point, next_point;
            now_point = new PointLatLng(cul_lat, cul_lng);
            markers_now = new GMapMarkerImage(now_point, m_Img[3]);
            //Img_plane = m_Img[0];
            //img_plane1 = m_Img[0];
            //Img_plane = RotateImg(img_plane1, 30);
    
            //m_Img[0].RotateFlip(RotateFlipType.Rotate180FlipY);
            plane_route = new GMapMarkerImage(now_point, m_Img[0]);
            //overlay_draw.Markers.Clear();
            overlay_plane.Markers.Clear();
            overlay_draw.Markers.Add(markers_now);
            overlay_plane.Markers.Add(plane_route);
            
            

            cul_lat += 0.003;
            cul_lng += 0.005;


            next_point = new PointLatLng(last_lat, last_lng);
            points1.Add(now_point);
            points1.Add(next_point);

            if (index == 1)
            {
                index = 2;
            }
            else
            {
                groute = new GMapRoute(points1, "route001");

                overlay_draw.Routes.Add(groute);
            }
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            double lat_show, lng_show;
            PointLatLng point_show;
            if ((this.textBox_lat.Text == null) & (this.textBox_lng.Text == null))
            {
                MessageBox.Show("1");
                return;
            
            }
        
            lat_show = Convert.ToDouble(textBox_lat.Text);
            lng_show = Convert.ToDouble(textBox_lng.Text);
            point_show = new PointLatLng(lat_show, lng_show);
            this.overlay_draw.Markers.Add(new GMapMarkerGoogleGreen(point_show));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            points_route.Clear();
            //points1.Clear();


            this.timer1.Stop();
            this.Overlay_Route.Markers.Clear();
            this.Overlay_Route.Routes.Clear();
            this.Overlay_Route.Polygons.Clear();

            this.Overlay_Poly.Markers.Clear();
            this.Overlay_Poly.Routes.Clear();
            this.Overlay_Poly.Polygons.Clear();

            this.overlay_plane.Markers.Clear();


            this.overlay_draw.Markers.Clear();
            this.overlay_draw.Routes.Clear();
            this.overlay_draw.Polygons.Clear();
            cul_lat = 31.929073;
            cul_lng = 118.769073;
            
        }

        GMapMarker markers_route;

        private int maxRoutePointNum = 20;
        private PointLatLng curPoint = new PointLatLng();
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            markers_route = new GMapMarkerImage(curPoint, m_Img[2]);//设置标记图案
            if (points_route.Count < maxRoutePointNum)
            {
                points_route.Add(markers_route);
            }
            else
            {
                points_route.RemoveAt(0);
                points_route.Add(markers_route);
            }

            Overlay_Route.Markers.Add(new GMapMarkerGoogleGreen(curPoint));
            Overlay_Route.Markers.Clear();                 
            for (int i = 0; i < points_route.Count; i++)
            {
                //MessageBox.Show("1");
                Overlay_Route.Markers.Add(points_route[i]);//把标记点加到图层上
            }
        }


        private int selectedIndex = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != selectedIndex)
            {
                selectedIndex = this.comboBox1.SelectedIndex;

                Change_mapProvider(selectedIndex);
            }
        }

        public void Change_mapProvider(int index)
        {//按索引更改地图来源，可用作外界的接口函数
            if (index == 0)
            {
                gMapProviders = GMap.NET.MapProviders.BingHybridMapProvider.Instance;
            }
            else if (index == 1)
            {
                gMapProviders = GMap.NET.MapProviders.BingMapOldProvider.Instance;
            }
            else if (index == 2)
            {
                gMapProviders = GMap.NET.MapProviders.BingMapProvider.Instance;
            }
            else if (index == 3)
            {
                gMapProviders = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
            }
            else if (index == 4)
            {
                gMapProviders = GMap.NET.MapProviders.OpenCycleMapProvider.Instance;
            }
            else if (index == 5)
            {
                gMapProviders = GMap.NET.MapProviders.OpenSeaMapHybridProvider.Instance;
            }
            else if (index == 6)
            {
                gMapProviders = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            }
            else if (index == 7)
            {
                gMapControl1.MapProvider = AmapProvide.Instance;
            }
            else
            {
                gMapProviders = GMap.NET.MapProviders.BingMapProvider.Instance;
            }

            this.gMapControl1.MapProvider = gMapProviders;
            
        }

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            curPoint = new PointLatLng();
            curPoint = this.gMapControl1.FromLocalToLatLng(e.X, e.Y);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Draw_routes(points_route, ref Overlay_Route);
        }

        private void Draw_routes(List<GMapMarker> points_x, ref GMapOverlay Overlay_x)
        {
            if (points_x.Count < 2) return;                         //少于两个点则无法绘制轨迹，直接返回

            Overlay_x.Routes.Clear();
            List<PointLatLng> points = new List<PointLatLng>();
            for (int i = 0; i < points_x.Count; i++)
            {
                points.Add(points_x[i].Position);
            }
            GMapRoute groute = new GMapRoute(points, "route001");
            Overlay_x.Routes.Add(groute);

           
        }
        GMapMarker markers_polygon;
        private int maxPointNum_polygon = 10;
        List<GMapMarker> points_polygon = new List<GMapMarker>();  
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            markers_polygon = new GMapMarkerImage(curPoint, m_Img[3]);
            if (points_polygon.Count < maxPointNum_polygon)
            {
                points_polygon.Add(markers_polygon);
            }
            else
            {
                points_polygon.RemoveAt(0);
                points_polygon.Add(markers_polygon);
            }

            Overlay_Poly.Markers.Add(new GMapMarkerGoogleGreen(curPoint));
            Overlay_Poly.Markers.Clear();                 //此句用于清除先前的mark
            for (int i = 0; i < points_polygon.Count; i++)
            {
                Overlay_Poly.Markers.Add(points_polygon[i]);
            }

        }
        //List<GMapMarker> points_polygon = new List<GMapMarker>(); 
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Draw_Polygons(points_polygon, ref Overlay_Poly);
        }
        private void Draw_Polygons(List<GMapMarker> points_x, ref GMapOverlay Overlay_x)
        {
            if (points_x.Count < 3) return;

            Overlay_x.Polygons.Clear();
            List<PointLatLng> points = new List<PointLatLng>();
            for (int i = 0; i < points_x.Count; i++)
            {
                points.Add(points_x[i].Position);
            }
            GMapPolygon polygon = new GMapPolygon(points, "myPolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(20, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            Overlay_x.Polygons.Add(polygon);
            this.gMapControl1.Overlays.Add(Overlay_x);

        }
        public Image RotateImg(Image b, float angle)
        {
            //Bitmap bmpsource=new Bitmap(img);
            //Bitmap bmpsrc=new Bitmap(bmpsource.Width,bmpsource.Height);
            //Rectangle rect = new Rectangle(0, 0, bmpsource.Width, bmpsource.Height);
            //Graphics g = Graphics.FromImage(bmpsource);
            //g.RotateTransform(angle);
            //g.DrawImage(bmpsrc,rect);
            //g.Dispose();
            ////MessageBox.Show("1");
            //return bmpsrc;
            angle = angle % 360;
            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //保存旋转后的图片
            b.Dispose();
            //dsImage.Save("FocusPoint.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }
    }
}
