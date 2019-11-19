// pages/nearShop/index.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    inputShowed: false,
    inputVal: "", // 搜索框内容
    banners: [
      {
        "pic": "../../images/banner/b1.png"
      },
      {
        "pic": "../../images/banner/b2.png"
      },
      {
        "pic": "../../images/banner/b3.png"
      }, {
        "pic": "../../images/banner/b4.png"
      }
    ],
    shops: [
      {
        "id": 1,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      },
      {
        "id": 2,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦,美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      },
      {
        "id": 3,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      },
      {
        "id": 4,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      },
      {
        "id": 5,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      },
      {
        "id": 6,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      },
      {
        "id": 7,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      }
    ],
    dataList: [
      {
        "id": 1,
        "title": "蔚县Now今日上线"
      },
      {
        "id": 2,
        "title": "蔚县人自己的微服务平台"
      }
    ],
    indicatorDots: true,
    autoplay: true,
    interval: 5000,
    duration: 1000,
    circular: true,
    sideMargin: '1rpx',
    showLoading: false
  },
  toShopDetail: function (options) {
    let value = options.currentTarget.dataset;
    console.log('nearShopToShopDetail', value)
    let shopInfo = JSON.stringify(value.item);
    let navigateUrl = `../nearShop/shopDetail/index?shopInfo=${shopInfo}`;
    wx.navigateTo({
      url: navigateUrl
    })
    console.log(value);
  },
  // 以下为搜索框事件
  showInput: function () {
    this.setData({
      inputShowed: true
    });
  },
  hideInput: function () {
    this.setData({
      inputVal: "",
      inputShowed: false
    });
  },
  clearInput: function () {
    this.setData({
      inputVal: ""
    });
  },
  inputTyping: function (e) {
    this.setData({
      inputVal: e.detail.value
    });
  },
  toSearch: function () {
    console.log('search...', this.data.inputVal)
    // this.setData({
    //   curPage: 1
    // });
    // this.getGoodsList(this.data.activeCategoryId);
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

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