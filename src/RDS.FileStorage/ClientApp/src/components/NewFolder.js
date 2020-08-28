import React, {Component} from 'react';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import IconButton from '@material-ui/core/IconButton';
import CreateNewFolderIcon from '@material-ui/icons/CreateNewFolder';


export class NewFolder extends Component {
    constructor(props) {
        super(props);

        this.state = {
            showDialog: false,
            nameWritten: false,
            folderName: ''
        }
    }

    handleClose = () => this.setState({showDialog: false});
    handleShow = () => this.setState({showDialog: true});

    nameWritten = e => {
        console.log(e.target.value);
        this.setState({
            folderName: e.target.value,
            nameWritten: true
        });
    }

    onSubmit = async e => {
        
        var folder = {
            name: this.state.folderName,
            fullPath: this.props.dir
        };
    
        console.log(folder);

        await fetch('directory/newfolder',
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(folder)
        })
        .then((response) => {
            if(!response.ok) throw new Error(response.status);
            else {
                console.log("jei");
                return response.json();
            }
        })
        .catch((error) => {
            console.error(error);
        });

        this.handleClose();

        this.props.callback();
    }

    render() {
        return(
            <>
            <IconButton onClick={this.handleShow}>
                <CreateNewFolderIcon/>
            </IconButton>
            <Dialog open={this.state.showDialog} onClose={this.handleClose}>
                <DialogTitle>Add New Folder</DialogTitle>
                <DialogContent>
          <DialogContentText>
            Please the name of the new folder. 
          </DialogContentText>
          <TextField
            autoFocus
            onChange={this.nameWritten}
            margin="dense"
            id="name"
            label={this.state.folderName}
            type="text"
            fullWidth
          />
        </DialogContent>
        <DialogActions>
          <Button color="primary" onClick={this.handleClose}>
            Cancel
          </Button>
          <Button color="primary" disabled={!this.state.nameWritten} type="submit" onClick={this.onSubmit}>
            Ok
          </Button>
        </DialogActions>
            </Dialog>
        </>
        )

    }
}