const App = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    trips: [
      {
        "id": 1,
        "name": "空中草原",
        "cover_image_w640": "https://cn.bing.com/th?id=OIP.05aGhTirRSES_iUYXPOIEwHaEo&pid=Api&rs=1",
        "date_added": "2019-10-28",
        "day_count": 10,
        "view_count": 10000,
        "popular_place_str": "来蔚县 带你领略大美 空中草原",
        "user": {
          "name": "鸟窝",
          "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
        }
      },
      {
        "id": 2,
        "name": "xiao niao",
        "cover_image_w640": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "date_added": 50,
        "day_count": 10,
        "view_count": 10000,
        "popular_place_str": "popular_place_str",
        "user": {
          "name": "bobo",
          "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
        }
      },
      {
        "id": 3,
        "name": "xiao niao",
        "cover_image_w640": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "date_added": 50,
        "day_count": 10,
        "view_count": 10000,
        "popular_place_str": "popular_place_str",
        "user": {
          "name": "bobo",
          "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
        }
      },
      {
        "id": 4,
        "name": "xiao niao",
        "cover_image_w640": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "date_added": 50,
        "day_count": 10,
        "view_count": 10000,
        "popular_place_str": "popular_place_str",
        "user": {
          "name": "bobo",
          "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
        }
      },
      {
        "id": 5,
        "name": "xiao niao",
        "cover_image_w640": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "date_added": 50,
        "day_count": 10,
        "view_count": 10000,
        "popular_place_str": "popular_place_str",
        "user": {
          "name": "bobo",
          "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
        }
      },
      {
        "id": 6,
        "name": "xiao niao",
        "cover_image_w640": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "date_added": 50,
        "day_count": 10,
        "view_count": 10000,
        "popular_place_str": "popular_place_str",
        "user": {
          "name": "bobo",
          "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
        }
      }
    ],
    start: 0,
    loading: false,
    windowWidth: App.systemInfo.windowWidth,
    windowHeight: App.systemInfo.windowHeight,
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },
  loadMore: function (e, needRefresh) {
    console.log('loadMore');
  },
  viewTrip(e) {
    const ds = e.currentTarget.dataset;
    let dt = JSON.stringify(ds.dt);
    wx.navigateTo({
      url: `./trip/trip?id=${ds.id}`,
    });
  },
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})