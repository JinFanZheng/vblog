import { Component } from 'react';
import { connect } from 'dva';
import withRouter from 'umi/withRouter';
import styles from './index.css';

class BasicLayout extends Component {
  render() {
    return (
      <div className={styles.normal}>
        <h1 className={styles.title}>Yay! Welcome to umi!</h1>
        {this.props.children}
      </div>
    );
  }
}

export default withRouter(connect(({ global }) => ({ global }))(BasicLayout));
