// pages/nearShop/shopDetail/index.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    shopDetail: [
      {
        "pic": 'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
        "content": "活动介绍 优惠大放送 大放送 来吧 来吧来吧"
      },
      {
        "pic": 'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
        "content": "活动介绍 优惠大放送 大放送 来吧 来吧来吧"
      },
      {
        "pic": 'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
        "content": "活动介绍 优惠大放送 大放送 来吧 来吧来吧"
      },
      {
        "pic": 'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
        "content": "活动介绍 优惠大放送 大放送 来吧 来吧来吧"
      }
    ],
    shopInfo: {}
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    let _shopInfo = JSON.parse(options.shopInfo);
    this.setData({
      shopInfo: _shopInfo
    })
  },
  openShopMap: function (e) {
    wx.getLocation({
      type: "gcj02",
      success(res) {
        console.log('res=', res)
        const latitude = res.latitude
        const longitude = res.longitude
        wx.openLocation({
          latitude,
          longitude,
          scale: 18
        })
      }
    })
  },
  callShopPhone: function (e) {
    let phoneNumber = this.data.shopInfo.telphone;
    wx.showModal({
      title: '拨打电话提示',
      content: `你将要给:${phoneNumber}拨打电话`,
      success(res) {
        if (res.confirm) {
          wx.makePhoneCall({
            phoneNumber: phoneNumber
          })
        }
      }
    })
  },
  previewImg: function (e) {
    console.log(e.currentTarget.dataset.index);
    var index = e.currentTarget.dataset.index;
    var imgArr = [];
    this.data.shopDetail.map((v, i) => {
      imgArr.push(v.pic)
    })
    wx.previewImage({
      current: imgArr[index],     //当前图片地址
      urls: imgArr,               //所有要预览的图片的地址集合 数组形式
      success: function (res) { },
      fail: function (res) { },
      complete: function (res) { },
    })
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
  onShareAppMessage: function (options) {
    console.log(options)
  }
})