import React, { Component } from 'react'
import echarts from 'echarts'
import { Col, Row, Icon } from 'antd'
import GApp from '../configs/models/ConfigInfoModel';
const Colors=GApp.color;
export default class Home extends Component {
    constructor(props) {
        super(props)
        this.clientChartElement = null;
    }

    componentDidMount() {
        this.initCharts()
    }
    initCharts = () => {
        // 初始化用户图表组件
        !this.clientChartElement && this.clientChart && (this.clientChartElement = echarts.init(this.clientChart, {}, { width: this.clientChart.clientWidth, height: this.clientChart.clientWidth * 0.33 }));
        this.setClientChartElement();
    }

    /**
     * 设置用户图表
     */
    setClientChartElement() {
        // 指定图表的配置项和数据
        var option = {
            // 标题
            title: { id: "clientChart", text: '最近一周用户新增', textStyle: { color: Colors.C1 }, padding: [20, 20, 20, 20] },
            xAxis: { type: 'category', data: ['2019-10-1','2019-10-2','2019-10-3','2019-10-4','2019-10-5'], axisLine: { lineStyle: { color: Colors.C0 } }, axisLabel: { color: Colors.C1 } },
            yAxis: { type: "value", splitNumber: 5, axisLine: { lineStyle: { color: Colors.C0 } }, axisLabel: { color: Colors.C1 }, splitLine: { lineStyle: { color: Colors.C0, width: 0.5, type: "dashed" } } },
            tooltip: {
                textStyle: { fontSize: 14, color: Colors.C8 },
            },
            toolbox: {
                feature: {
                    saveAsImage: {
                        pixelRatio: 2
                    }
                },
                right: 20,
            },
            legend: {
                padding: [20, 20, 20, 20],
                data: [
                    {
                        name: "用户每日新增", icon: "circle", textStyle: { color: "#e5a8ed" }
                    }
                ],
            },
            series: [{
                type: 'line',
                name: '用户每日新增',
                smooth: true,
                itemStyle: {
                    normal: {
                        color: '#e5a8ed',
                        lineStyle: {
                            color: '#e5a8ed'
                        }
                    }
                },
                data: [100,80,200,400,350]
            }]
        };

        this.clientChartElement.setOption(option);
    }

    renderClientChart = () => {
        return (
            <Row type="flex" justify="start">
                <Col span={18}>
                    <div ref={e => { if (e) this.clientChart = e }} style={styles.chart} />
                </Col>
                <div style={{ ...styles.chartBar, flex: 1, backgroundColor: '#e5a8ed60' }}>
                    <div style={{ marginRight: 10, padding: '25px 25px 25px 25px', display: 'flex', flex: 1, flexDirection: 'row', justifyContent: 'flex-start', alignItems: 'center', background: '#fff' }}>
                        <Icon type={"rocket"} style={{ fontSize: 32, color: "#e5a8ed" }} />
                        <div style={{ display: 'flex', flexDirection: 'column', justifyContent: 'center', marginLeft: 15 }}>
                            <p style={{ fontSize: 16 }}>{`用户总量 ${100}`}</p>
                            <p style={{ fontSize: 14 }}>{`实名用户 ${200}`}</p>
                            <p style={{ fontSize: 14 }}>{`总交易手续费 ${300}`}</p>
                            <p style={{ fontSize: 14 }}>{`今日交易手续费 ${400}`}</p>
                        </div>
                    </div>
                </div>
            </Row>
        )
    }
    render() {
        return (
            <div>
                {this.renderClientChart()}
            </div>
        )
    }
}

const styles = {
    chart: { marginTop: 10, backgroundColor: '#FFFFFF' },
    chartBar: { display: 'flex', flex: 1, margin: '10px 0 0 10px' },
}
