import { Component } from 'react';
import { connect } from 'dva';
import { Table, Divider, Tag, Button, Popconfirm } from 'antd';
import Link from 'umi/link';

const columns = [
  {
    title: '标题',
    dataIndex: 'title',
    key: 'title',
    render: (text, record) => <Link to={`/article/edit/?guid=${record.guid}`}>{text}</Link>,
  },
  {
    title: '分类',
    dataIndex: 'category',
    key: 'category',
  },
  {
    title: '摘要',
    dataIndex: 'summary',
    key: 'summary',
  },
  {
    title: '标签',
    key: 'tag',
    dataIndex: 'tag',
    render: tags => (
      <span>
        {tags.split(',').map(tag => (
          <Tag color="blue" key={tag}>
            {tag}
          </Tag>
        ))}
      </span>
    ),
  },
  {
    title: '启用',
    dataIndex: 'isEnabled',
    key: 'isEnabled',
    render: p => (
      <span>
        <Tag color={p ? 'green' : 'red'} key={p}>
          {p ? '是' : '否'}
        </Tag>
      </span>
    ),
  },
  {
    title: '允许评论',
    dataIndex: 'canComment',
    key: 'canComment',
    render: p => (
      <span>
        <Tag color={p ? 'green' : 'red'} key={p}>
          {p ? '是' : '否'}
        </Tag>
      </span>
    ),
  },
  {
    title: '排序',
    dataIndex: 'sort',
    key: 'sort',
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
  },
  {
    title: '操作',
    key: 'action',
    render: (text, record) => (
      <span>
        <Link to={`/article/edit/?guid=${record.guid}`}>
          <Tag color={'#108ee9'}>编辑</Tag>
        </Link>
        {/* <Divider type="vertical" /> */}
        <Popconfirm
          placement="bottomRight"
          title={'确定要删除吗?'}
          onConfirm={() => {
            console.log(record);
          }}
          okText="是"
          cancelText="否"
        >
          <Tag color={'#f50'}>删除</Tag>
        </Popconfirm>
      </span>
    ),
  },
];

class Article extends Component {
  render() {
    const { article } = this.props;
    const { pages } = article;
    const { count, list } = pages;
    return (
      <div>
        <div>
          <Link to={'/article/edit'}>
            <Button type="primary">写博客</Button>
          </Link>
        </div>
        <Table columns={columns} dataSource={list} />
      </div>
    );
  }
}

export default connect(({ article }) => ({ article }))(Article);
