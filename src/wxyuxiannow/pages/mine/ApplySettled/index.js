const Api = require('.././../../utils/httpPost.js');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    openTime: "10:00",
    closeTime: "20:00",
    phoneNum: '',
    title: '',
    content: '',
    images: [],
    base64Imgs: '',
    latitude: '',
    longitude: ''
  },
  bindAreaBlur: function (e) {
    this.setData({
      content: e.detail.value
    })
  },
  bindOpenTimeChange: function (params) {
    this.setData({
      openTime: params.detail.value
    })
  },
  bindCloseTimeChange: function (params) {
    this.setData({
      closeTime: params.detail.value
    })
  },
  bindPhone: function (params) {
    let phone = params.detail.value;
    this.setData({
      phoneNum: phone
    })
  },
  bindShopName: function (params) {
    this.setData({
      title: params.detail.value
    })
  },
  chooseImage: function (e) {
    var selectPictureNum = e.target.dataset.num;
    if (selectPictureNum >= 1) {
      wx.showModal({
        title: '提示',
        content: '最多上传1张店铺logo',
        cancelText: '取消'
      })
      return;
    }
    var that = this;
    wx.chooseImage({
      sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
      sourceType: ['camera'], // 可以指定来源是相册还是相机，默认二者都有
      success: function (res) {
        var tempFilePaths = res.tempFilePaths;
        that.setData({
          images: that.data.images.concat(tempFilePaths)
        })
        that.uploadImg(tempFilePaths);
      }
    })

  },
  uploadImg: function (uploadImgs) {
    uploadImgs.map((v) => {
      this.uploadBase64File(v);
    });

  },
  uploadBase64File: function (v) {
    wx.getFileSystemManager().readFile({
      filePath: v, //选择图片返回的相对路径
      encoding: 'base64', //编码格式
      success: (res) => {
        let baseImg = 'data:image/png;base64,' + res.data
        this.setData({
          base64Imgs: baseImg
        })
      }
    })
  },
  pubMessage: function () {
    let that = this;
    wx.showLoading()
    let data = {};
    data.logoPic = that.data.base64Imgs;
    data.longitude = that.data.longitude;
    data.latitude = that.data.latitude;
    data.openTime = that.data.openTime;
    data.closeTime = that.data.closeTime;
    data.phoneNum = that.data.phoneNum;
    data.title = that.data.title;
    data.content = that.data.content;
    Api.Post('/api/PubShop', data).then(res => {
      wx.hideLoading();
      if (res.code == 200) {
        wx.showModal({
          title: '提示',
          content: '提交审核成功',
          confirmText: '确定',
          success(res) {
            if (res.confirm) {
              wx.navigateBack()
            }
          }
        })
      } else {
        wx.showModal({
          title: '提示',
          content: '提交审核失败' + res.message
        })
      }
    })
  },
  onLoad: function (params) {
    var thta = this;
    wx.getLocation({
      type: "gcj02",
      success(res) {
        const latitude = res.latitude
        const longitude = res.longitude
        thta.setData({
          longitude: longitude,
          latitude: latitude
        })
      }
    })
  }
})