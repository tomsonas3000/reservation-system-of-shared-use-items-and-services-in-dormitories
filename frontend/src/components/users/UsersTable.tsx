import {
  Box,
  Button,
  Modal,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TableSortLabel,
  Typography,
} from '@mui/material';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UsersService } from '../../services/usersService';
import Role from '../../utils/enums/role';
import { UserType } from './types/UserType';

const UsersTable = (props: {
  data: UserType[];
  onBanUpdate: () => void;
}): JSX.Element => {
  const modalContentStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    p: 2,
  };

  const navigate = useNavigate();

  type Order = 'asc' | 'desc';

  const [banModalOpen, setBanModalOpen] = useState(false);
  const [removeBanModalOpen, setRemoveBanModalOpen] = useState(false);
  const [selectedUserId, setSelectedUserId] = useState('');
  const [order, setOrder] = useState<Order>('asc');
  const [orderBy, setOrderBy] = useState<keyof UserType>('name');

  const handleUserBanClick = (id: string) => {
    setBanModalOpen(true);
    setSelectedUserId(id);
  };

  const handleRemoveUserBanClick = (id: string) => {
    setRemoveBanModalOpen(true);
    setSelectedUserId(id);
  };

  const handleUserBanConfirmClick = () => {
    UsersService.banUserFromReserving(selectedUserId).then(() => {
      setBanModalOpen(false);
      props.onBanUpdate();
    });
  };

  const handleUserBanRemoveConfirmClick = () => {
    UsersService.removeReservationBan(selectedUserId).then(() => {
      setRemoveBanModalOpen(false);
      props.onBanUpdate();
    });
  };

  const handleSort = (property: keyof UserType) => {
    setOrder(order === 'asc' ? 'desc' : 'asc');
    setOrderBy(property);
  };

  return (
    <>
      <Modal open={banModalOpen} onClose={() => setBanModalOpen(false)}>
        <Box sx={modalContentStyle}>
          <Typography variant="h6">
            Are you sure you want to ban this student from reservating?
          </Typography>
          <Box sx={{ justifyContent: 'flex-end', display: 'flex', m: 2 }}>
            <Button
              sx={{ mx: 1 }}
              variant="contained"
              color="error"
              onClick={handleUserBanConfirmClick}>
              Yes
            </Button>
            <Button
              sx={{ mx: 1 }}
              variant="contained"
              color="success"
              onClick={() => setBanModalOpen(false)}>
              No
            </Button>
          </Box>
        </Box>
      </Modal>
      <Modal
        open={removeBanModalOpen}
        onClose={() => setRemoveBanModalOpen(false)}>
        <Box sx={modalContentStyle}>
          <Typography variant="h6">
            Are you sure you want to remove the reservation ban for this
            student?
          </Typography>
          <Box sx={{ justifyContent: 'flex-end', display: 'flex' }}>
            <Button
              sx={{ mx: 1 }}
              variant="contained"
              color="error"
              onClick={handleUserBanRemoveConfirmClick}>
              Yes
            </Button>
            <Button
              sx={{ mx: 1 }}
              variant="contained"
              color="success"
              onClick={() => setRemoveBanModalOpen(false)}>
              No
            </Button>
          </Box>
        </Box>
      </Modal>
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 1300 }}>
        <Table sx={{ my: 4 }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#81d4fa' }}>
              <TableCell sortDirection={orderBy === 'name' ? order : false}>
                <TableSortLabel
                  active={orderBy === 'name'}
                  direction={orderBy === 'name' ? order : 'asc'}
                  onClick={() => handleSort('name')}>
                  Name
                </TableSortLabel>
              </TableCell>
              <TableCell sortDirection={orderBy === 'surname' ? order : false}>
                <TableSortLabel
                  active={orderBy === 'surname'}
                  direction={orderBy === 'surname' ? order : 'asc'}
                  onClick={() => handleSort('surname')}>
                  Surname
                </TableSortLabel>
              </TableCell>
              <TableCell>Telephone number</TableCell>
              <TableCell></TableCell>
              <TableCell sortDirection={orderBy === 'role' ? order : false}>
                <TableSortLabel
                  active={orderBy === 'role'}
                  direction={orderBy === 'role' ? order : 'asc'}
                  onClick={() => handleSort('role')}>
                  Role
                </TableSortLabel>
              </TableCell>
              <TableCell></TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data
                .sort((a: UserType, b: UserType) => {
                  const comparatorValue = (a: UserType, b: UserType) => {
                    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                    // @ts-ignore: Object is possibly 'null'.
                    if (b[orderBy] < a[orderBy]) {
                      return -1;
                    }
                    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                    // @ts-ignore: Object is possibly 'null'.
                    if (b[orderBy] > a[orderBy]) {
                      return 1;
                    }
                    return 0;
                  };
                  return order === 'desc'
                    ? comparatorValue(a, b)
                    : -comparatorValue(a, b);
                })
                .map((user: UserType, index: number) => {
                  return (
                    <TableRow
                      key={index}
                      sx={{
                        backgroundColor:
                          user.hasMoreThanTenReservations &&
                          user.role === Role.Student
                            ? '#fff176'
                            : '#e1f5fe',
                        height: 70,
                      }}>
                      <TableCell>{user.name}</TableCell>
                      <TableCell>{user.surname}</TableCell>
                      <TableCell>{user.telephoneNumber}</TableCell>
                      <TableCell>
                        <Button
                          variant="contained"
                          onClick={() =>
                            navigate(`/users/send-email/${user.emailAddress}`)
                          }
                          sx={{
                            minWidth: 150,
                            backgroundColor: '#009688',
                            ':hover': {
                              backgroundColor: '#00897b',
                            },
                          }}>
                          Send email
                        </Button>
                      </TableCell>
                      <TableCell>{user.role}</TableCell>
                      <TableCell>
                        {user.role === Role.Student && (
                          <Box
                            sx={{
                              display: 'flex',
                              justifyContent: 'flex-end',
                              maxWidth: 150,
                            }}>
                            <Button
                              variant="outlined"
                              onClick={() =>
                                navigate(`/user-reservations/${user.id}`)
                              }>
                              Reservations
                            </Button>
                          </Box>
                        )}
                      </TableCell>
                      <TableCell>
                        <Box
                          sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                          {user.role === Role.Student &&
                            !user.isBannedFromReserving && (
                              <Button
                                variant="contained"
                                sx={{ minWidth: 230 }}
                                color="error"
                                onClick={() =>
                                  handleUserBanClick(user.id as string)
                                }>
                                Ban from reservating
                              </Button>
                            )}
                          {user.role === Role.Student &&
                            user.isBannedFromReserving && (
                              <Button
                                variant="contained"
                                sx={{
                                  minWidth: 230,
                                  backgroundColor: '#ff9800',
                                  ':hover': {
                                    backgroundColor: '#fb8c00',
                                  },
                                }}
                                onClick={() =>
                                  handleRemoveUserBanClick(user.id as string)
                                }>
                                Remove reservation ban
                              </Button>
                            )}
                        </Box>
                      </TableCell>
                    </TableRow>
                  );
                })}
            </TableBody>
          }
        </Table>
      </TableContainer>
    </>
  );
};

export default UsersTable;
