using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;

namespace gmap_test
{
    public abstract class GmapProviderExt : GMapProvider
    {
        public GmapProviderExt()
        {
            MaxZoom = null;
            RefererUrl = "http://map.soso.com";
        }

        public override PureProjection Projection
        {
            get { return MercatorProjection.Instance; }
        }
        GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
                {
                    overlays = new GMapProvider[] { this };
                }
                return overlays;
            }
        }
    }
    public class AmapProvide : GmapProviderExt
    {
        public static readonly AmapProvide Instance;

        //readonly Guid id = new Guid("EF3DD303-3F74-4938-BF40-232D0595EE88");
        readonly Guid id = new Guid("30D069B3-F6A5-4feb-9B5E-84CBF1E34F34");
        public override Guid Id
        {
            get { return id; }
        }

        readonly string name = "AMap";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        static AmapProvide()
        {
            Instance = new AmapProvide();
        }
        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            try
            {
                string url = MakeTileImageUrl(pos, zoom, LanguageStr);
                return GetTileImageUsingHttp(url);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            var num = (pos.X + pos.Y) % 4 + 1;
            //string url = string.Format(UrlFormat, num, pos.X, pos.Y, zoom);
            string url = string.Format(UrlFormat, pos.X, pos.Y, zoom);
            return url;

        }
        static readonly string UrlFormat = "http://p0.map.soso.com/maptilesv2/{0}/{1}/{2}/{3}_{4}.png";

    }







    //public class AMapProvider : AMapProviderBase
    //{
    //    private readonly string name = "AMap";
    //    private readonly string language = "zh_cn";
    //    private readonly Guid id = new Guid("F81F5FB4-0902-4686-BF5B-B2B1E4D47922");
    //    public static readonly AMapProvider Instance;
    //    private Random ran = new Random();
    //    private static string UrlFormat = "http://webrd0{0}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&style=7&x={1}&y={2}&z={3}&scale=1&ltype=3";
    //    public string Caption
    //    {
    //        get
    //        {
    //            return "高德地图";
    //        }
    //    }
    //    public override Guid Id
    //    {
    //        get { return this.id; }
    //    }

    //    public override string Name
    //    {
    //        get { return this.name; }
    //    }

    //    static AMapProvider()
    //    {
    //        Instance = new AMapProvider();
    //    }
    //    public AMapProvider()
    //    {

    //    }

    //    public override PureImage GetTileImage(GPoint pos, int zoom)
    //    {
    //        string url = MakeTileImageUrl(pos, zoom, language);
    //        return GetTileImageUsingHttp(url);
    //    }
    //    //http://wprd0{0}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&style=7&x={1}&y={2}&z={3}&scl=2&ltype=3
    //    private string MakeTileImageUrl(GPoint pos, int zoom, string language)
    //    {
    //        int serverID = ran.Next(1, 5);//1-4 
    //        return string.Format(UrlFormat, 4, pos.X, pos.Y, zoom);
    //    }
    //}
    //public abstract class AMapProviderBase : GMapProvider
    //{
    //    protected GMapProvider[] overlays;
    //    public AMapProviderBase()
    //    {
    //        RefererUrl = "http://www.amap.com/";
    //        Copyright = string.Format("©{0} 高德地图 GPRS(@{0})", DateTime.Today.Year);
    //        MinZoom = 1;
    //        MaxZoom = 20;
    //    }

    //    public override GMapProvider[] Overlays
    //    {
    //        get
    //        {
    //            if (overlays == null)
    //            {
    //                overlays = new GMapProvider[] { this };
    //            }
    //            return overlays;
    //        }
    //    }

    //    public override PureProjection Projection
    //    {
    //        get
    //        {
    //            return MercatorProjection.Instance;
    //        }
    //    }
    //}
}
