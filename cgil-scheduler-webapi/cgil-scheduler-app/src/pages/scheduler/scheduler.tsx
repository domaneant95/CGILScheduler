import React from 'react';
import Scheduler, { Resource, Scrolling, View } from 'devextreme-react/scheduler';
import RadioGroup from 'devextreme-react/radio-group';

import {
  data, assignees, rooms, priorities, resourcesList
} from '../../demo-data/data';
import { SpeedDialAction } from 'devextreme-react';

const currentDate = new Date(2021, 3, 27);
const views: string[] | any = ['day', "week", "workWeek", "month"];
const groups: string[] | any = ['roomId'];


class SchedulerContainer extends React.Component {
  schedulerRef: any = null;

  constructor(props: any) {
    super(props);
    this.state = {
      currentDate: new Date(2021, 2, 25),
      cellDuration: 30,
    };
    this.showAppointmentPopup = this.showAppointmentPopup.bind(this);
    this.filterBy = this.filterBy.bind(this);
    this.onOptionChanged = this.onOptionChanged.bind(this);

    this.schedulerRef = React.createRef();
  }

  onOptionChanged(e: any) {
    if (e.name === 'currentDate') {
      this.setState({ currentDate: e.value });
    }
  }

  showAppointmentPopup() {
    this.schedulerRef.current.instance.showAppointmentPopup();
  }

  filterBy() {

  }

  render(): React.ReactNode {
    return (
      <React.Fragment>
        <Scheduler
          ref={this.schedulerRef}
          timeZone="America/Los_Angeles"
          dataSource={data}
          //views={views}
          defaultCurrentView="day"
          defaultCurrentDate={currentDate}
          startDayHour={9}
          endDayHour={19}
          //groups={groups}
          adaptivityEnabled={true}
        >

          {/* <View
            type='timelineWorkWeek'
            name='Timeline'
            groupOrientation='vertical'
          /> */}

          <View
            type='day'
            groupOrientation='vertical'
          />
          
          <View
            type='week'
            groupOrientation='vertical'
          />
          <View
            type='month'
            groupOrientation='vertical'
          />

          <Resource
            dataSource={rooms}
            allowMultiple={true}
            fieldExpr="roomId"
            label="Sede"
            useColorAsDefault={true}
          />

          <Resource
            dataSource={assignees}
            allowMultiple={true}
            fieldExpr="assigneeId"
            label="Assignee"
            useColorAsDefault={true}
          />

        </Scheduler>
        <SpeedDialAction
          icon="plus"
          onClick={() => this.showAppointmentPopup()}
        />

        <Scrolling
          mode='virtual'
        />

      </React.Fragment>
    );
  }
}

export default function SchedulerPage() {
  return (<SchedulerContainer />)
}